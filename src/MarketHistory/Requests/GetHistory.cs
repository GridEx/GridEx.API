﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.MarketHistory.Responses;

namespace GridEx.API.MarketHistory.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct GetHistory : IHistoryRequest
	{
		public GetHistory(long requestId, ushort timeFrame, long lastBarTime)
		{
			Size = MessageSize;
			TypeCode = HistoryRequestTypeCode.GetHistory;
			RequestId = requestId;
			TimeFrame = timeFrame;
			LastBarTime = lastBarTime;
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HistoryRequestTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public long RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (GetHistory* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				var source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HistoryRejectReasonCode IsValid()
		{
			if (SupportedTimeFrames.IsValid(TimeFrame) && LastBarTime > 0)
			{
				return HistoryRejectReasonCode.Ok;
			}

			return HistoryRejectReasonCode.InvalidTimeFrame;
		}

		public readonly ushort TimeFrame;

		public readonly long LastBarTime;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<GetHistory>());
	}
}