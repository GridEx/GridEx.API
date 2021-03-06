﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct AccessTokenRejected : IHistoryResponse
	{
		public AccessTokenRejected(ApiVersion apiVersion, HistoryRejectReasonCode rejectCode)
		{
			Size = MessageSize;
			TypeCode = HistoryResponseTypeCode.AccessTokenRejected;
			ApiVersion = apiVersion;
			RejectCode = rejectCode;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly AccessTokenRejected CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((AccessTokenRejected*)source)[0];
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

		public readonly ApiVersion ApiVersion;
		public readonly HistoryRejectReasonCode RejectCode;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<AccessTokenRejected>());
	}
}
