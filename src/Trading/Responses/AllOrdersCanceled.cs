﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct AllOrdersCanceled : IHftResponse
	{
		public AllOrdersCanceled(long requestId, byte quantity)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.AllOrdersCanceled;
			RequestId = requestId;
			Quantity = quantity;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly AllOrdersCanceled CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((AllOrdersCanceled*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HftResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly long RequestId;
		public readonly byte Quantity;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<AllOrdersCanceled>());
	}
}
