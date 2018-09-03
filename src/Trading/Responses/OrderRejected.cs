using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct OrderRejected : IHftResponse
	{
		public OrderRejected(long requestId, HftRejectReasonCode rejectCode)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.OrderRejected;
			RequestId = requestId;
			RejectCode = rejectCode;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly OrderRejected CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((OrderRejected*)source)[0];
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
		public readonly HftRejectReasonCode RejectCode;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<OrderRejected>());
	}
}
