﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct OrderCancelled : IHftResponse
	{
		public OrderCancelled(long orderId, double unfilledVolume)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.OrderCancelled;
			OrderId = orderId;
			UnfilledVolume = unfilledVolume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly OrderCancelled CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((OrderCancelled*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public ResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly long OrderId;
		public readonly double UnfilledVolume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<OrderCancelled>());
	}
}
