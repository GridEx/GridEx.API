using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct OrderCreated : IHftResponse
	{
		public OrderCreated(int requestId, long orderId)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.OrderCreated;
			RequestId = requestId;
			OrderId = orderId;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (OrderCreated* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public unsafe static ref readonly OrderCreated CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((OrderCreated*)source)[0];
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

		public int RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly long OrderId;
		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<OrderCreated>());
	}
}