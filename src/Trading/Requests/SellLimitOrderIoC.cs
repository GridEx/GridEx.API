﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct SellLimitOrderIoC : IHftRequest
	{
		public SellLimitOrderIoC(long requestId, double price, double volume)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.SellLimitOrderIoC;
			RequestId = requestId;
			Price = price;
			Volume = volume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] buffer, int offset = 0)
		{
			fixed (SellLimitOrderIoC* thisAsPointer = &this)
			fixed (byte* target = &buffer[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RejectReasonCode IsValid()
		{
			if (Price < PriceRange.Min || Price > PriceRange.Max)
			{
				return RejectReasonCode.InvalidOrderPriceRange;
			}

			if (Volume < VolumeRange.Min || Volume > VolumeRange.Max)
			{
				return RejectReasonCode.InvalidOrderVolumeRange;
			}

			return RejectReasonCode.Ok;
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public RequestTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public long RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly double Price;
		public readonly double Volume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<SellLimitOrderIoC>());
	}
}