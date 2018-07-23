using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct OrderExecuted : IHftResponse
	{
		public OrderExecuted(long orderId, long executionTime, double price, double executedVolume, double unfilledVolume)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.OrderExecuted;
			OrderId = orderId;
			ExecutionTime = executionTime;
			Price = price;
			ExecutedVolume = executedVolume;
			UnfilledVolume = unfilledVolume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly OrderExecuted CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((OrderExecuted*)source)[0];
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

		public bool IsCompleted
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			// Calculations are performed on the server with much greater accuracy,
			// double type is used only as DTO, so zero is really zero.
			get { return UnfilledVolume == 0; }
		}

		public readonly long OrderId;
		public readonly long ExecutionTime;
		public readonly double Price;
		public readonly double ExecutedVolume;
		public readonly double UnfilledVolume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<OrderExecuted>());
	}
}