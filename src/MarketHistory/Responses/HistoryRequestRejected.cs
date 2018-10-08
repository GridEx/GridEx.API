using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct HistoryRequestRejected : IHistoryResponse
	{
		public HistoryRequestRejected(long requestId, HistoryRejectReasonCode rejectCode)
		{
			Size = MessageSize;
			TypeCode = HistoryResponseTypeCode.RequestRejected;
			RequestId = requestId;
			RejectCode = rejectCode;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly HistoryRequestRejected CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((HistoryRequestRejected*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HistoryResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly long RequestId;
		public readonly HistoryRejectReasonCode RejectCode;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<HistoryRequestRejected>());
	}
}