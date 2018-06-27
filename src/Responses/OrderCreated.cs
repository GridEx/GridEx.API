using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct OrderCreated : IHftResponse
	{
		public OrderCreated(long requestId, long orderId)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.OrderCreated;
			RequestId = requestId;
			OrderId = orderId;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly OrderCreated CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((OrderCreated*)source)[0];
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

		public long RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly long OrderId;
		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<OrderCreated>());
	}
}