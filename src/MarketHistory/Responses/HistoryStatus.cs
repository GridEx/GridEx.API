using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct HistoryStatus : IHistoryResponse
	{
		public HistoryStatus(long requestId, long lastChangeTime, long firstBarTime, long lastFilledBarTime, int barsQuantity)
		{
			Size = MessageSize;
			TypeCode = HistoryResponseTypeCode.History;
			RequestId = requestId;
			LastChangeTime = lastChangeTime;
			FirstBarTime = firstBarTime;
			LastFilledBarTime = lastFilledBarTime;
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
		public static unsafe ref readonly HistoryStatus CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((HistoryStatus*)source)[0];
			}
		}

		public readonly long RequestId;

		public readonly long LastChangeTime;

		public readonly long FirstBarTime;

		public readonly long LastFilledBarTime;

		public readonly int BarsQuantity;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<HistoryStatus>());
	}
}
