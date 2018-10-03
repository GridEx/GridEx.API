using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct LastHistory : IHistoryResponse
	{
		public LastHistory(long requestId, long creationTime, ushort timeFrame, byte barsQuantity)
		{
			Size = MessageSize;
			TypeCode = HistoryResponseTypeCode.LastHistory;
			RequestId = requestId;
			CreationTime = creationTime;
			TimeFrame = timeFrame;
			BarsQuantity = barsQuantity;
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
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref LastHistory CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((LastHistory*)source)[0];
			}
		}

		public long RequestId;

		public long CreationTime;

		public ushort TimeFrame;

		public byte BarsQuantity;

		public fixed long Time[HistoryLength];

		public fixed double Open[HistoryLength];

		public fixed double High[HistoryLength];

		public fixed double Low[HistoryLength];

		public fixed double Close[HistoryLength];

		public fixed double Volume[HistoryLength];

		public const int HistoryLength = 16;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<LastHistory>());
	}
}