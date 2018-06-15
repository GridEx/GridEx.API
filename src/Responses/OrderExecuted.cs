using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
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

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (OrderExecuted* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public static unsafe ref readonly OrderExecuted CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((OrderExecuted*)source)[0];
			}
		}

		public byte Size
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
			get { return UnfilledVolume == 0; }
		}

		public readonly long OrderId;
		public readonly long ExecutionTime;
		public readonly double Price;
		public readonly double ExecutedVolume;
		public readonly double UnfilledVolume;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<OrderExecuted>());
	}
}