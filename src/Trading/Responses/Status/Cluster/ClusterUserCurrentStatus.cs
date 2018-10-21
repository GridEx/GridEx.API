﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Responses.Cluster;

namespace GridEx.API.Trading.Responses.Status.Cluster
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ClusterUserCurrentStatus : IClusterHftResponse
	{
		public ClusterUserCurrentStatus(
			long userId,
			ref HftMarketSettings marketSettings,
			ref UserAssets userAssets,
			ref UserLiveOrders userLiveOrders)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.ClusterCurrentStatus;
			UserId = userId;
			MarketSettings = marketSettings;
			UserAssets = userAssets;
			UserLiveOrders = userLiveOrders;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref ClusterUserCurrentStatus CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((ClusterUserCurrentStatus*)source)[0];
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

		public long UserId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HftMarketSettings MarketSettings;

		public UserAssets UserAssets;

		public UserLiveOrders UserLiveOrders;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ClusterUserCurrentStatus>());
	}
}