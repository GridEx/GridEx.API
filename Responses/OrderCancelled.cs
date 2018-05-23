using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct OrderCancelled : IHftResponse
	{
		public OrderCancelled(Guid orderId)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.OrderCancelled;
			OrderId = orderId;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (OrderCancelled* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public static unsafe ref readonly OrderCancelled CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((OrderCancelled*)source)[0];
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

		public readonly Guid OrderId;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<OrderCancelled>());
	}
}
