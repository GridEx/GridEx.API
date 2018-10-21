using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading.Requests.Cluster
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct ClusterSellOrder : IClusterHftOrder
	{
		public ClusterSellOrder(long requestId, long userId, double volume)
		{
			Size = MessageSize;
			TypeCode = HftRequestTypeCode.ClusterSellOrder;
			RequestId = requestId;
			UserId = userId;
			Volume = volume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (ClusterSellOrder* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HftRejectReasonCode IsValid()
		{
			if (!VolumeRange.Instance.InSellSideRange(Volume))
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

		public double Volume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ClusterSellOrder>());
	}
}