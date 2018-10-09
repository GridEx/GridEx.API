using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct LastHistory : IHistoryResponse
	{
		public LastHistory(long requestId, long lastChangeTime, ushort timeFrame)
		{
			Size = MessageSize;
			TypeCode = HistoryResponseTypeCode.LastHistory;
			RequestId = requestId;
			LastChangeTime = lastChangeTime;
			TimeFrame = timeFrame;
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

		public long LastChangeTime;

		public ushort TimeFrame;

		public fixed long Time[LastHistoryLength];

		public fixed double Open[LastHistoryLength];

		public fixed double High[LastHistoryLength];

		public fixed double Low[LastHistoryLength];

		public fixed double Close[LastHistoryLength];

		public fixed double Volume[LastHistoryLength];

		public const int LastHistoryLength = 2;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<LastHistory>());
	}
}