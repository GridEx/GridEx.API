﻿using GridEx.API.Responses;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct SellOrder : IHftRequest
	{
		public SellOrder(int requestId, double volume)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.SellOrder;
			RequestId = requestId;
			Volume = volume;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (SellOrder* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public unsafe static ref readonly SellOrder CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((SellOrder*)source)[0];
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RejectReasonCode IsValid()
		{
			if (Volume < VolumeRange.Min || Volume > VolumeRange.Max)
			{
				return RejectReasonCode.InvalidOrderVolumeRange;
			}

			return RejectReasonCode.Ok;
		}

		public byte Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public RequestTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public int RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly double Volume;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<SellOrder>());
	}
}