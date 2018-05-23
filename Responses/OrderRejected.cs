using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct OrderRejected : IHftResponse
	{
		public OrderRejected(int requestId, RejectReasonCode rejectCode)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.OrderRejected;
			RequestId = requestId;
			RejectCode = rejectCode;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (OrderRejected* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public unsafe static ref readonly OrderRejected CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((OrderRejected*)source)[0];
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

		public readonly int RequestId;
		public readonly RejectReasonCode RejectCode;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<OrderRejected>());
	}
}
