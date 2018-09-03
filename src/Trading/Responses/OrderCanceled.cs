using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct OrderCanceled : IHftResponse
	{
		public OrderCanceled(long orderId, double unfilledVolume)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.OrderCanceled;
			OrderId = orderId;
			UnfilledVolume = unfilledVolume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly OrderCanceled CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((OrderCanceled*)source)[0];
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

		public readonly long OrderId;
		public readonly double UnfilledVolume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<OrderCanceled>());
	}
}
