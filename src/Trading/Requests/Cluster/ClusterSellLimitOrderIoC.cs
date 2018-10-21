﻿using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading.Requests.Cluster
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct ClusterSellLimitOrderIoC : IClusterHftLimitOrder
	{
		public ClusterSellLimitOrderIoC(long requestId, long userId, double price, double volume)
		{
			Size = MessageSize;
			TypeCode = HftRequestTypeCode.ClusterSellLimitOrderIoC;
			RequestId = requestId;
			UserId = userId;
			Price = price;
			Volume = volume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] buffer, int offset = 0)
		{
			fixed (ClusterSellLimitOrderIoC* thisAsPointer = &this)
			fixed (byte* target = &buffer[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HftRejectReasonCode IsValid()
		{
			if (!PriceRange.Instance.InRange(Price))
			{
				return HftRejectReasonCode.InvalidOrderPriceRange;
			}

			if (!VolumeRange.Instance.InSellSideRange(Volume, Price))
			{
				return HftRejectReasonCode.InvalidOrderVolumeRange;
			}

			if (UserId == 0)
			{
				return HftRejectReasonCode.InvalidUserId;
			}

			return HftRejectReasonCode.Ok;
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HftRequestTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public long RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public long UserId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public double Price
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public double Volume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ClusterSellLimitOrderIoC>());
	}
}