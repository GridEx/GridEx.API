using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading.Requests.Cluster
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct ClusterCancelAllOrders : IClusterHftRequest
	{
		public ClusterCancelAllOrders(long requestId, long userId, CancelAllOrdersFlags flags)
		{
			Size = MessageSize;
			TypeCode = HftRequestTypeCode.ClusterCancelAllOrders;
			RequestId = requestId;
			UserId = userId;
			Flags = flags;
		}

		public ClusterCancelAllOrders(long requestId, long userId)
		{
			Size = MessageSize;
			TypeCode = HftRequestTypeCode.ClusterCancelAllOrders;
			RequestId = requestId;
			UserId = userId;
			Flags = CancelAllOrdersFlags.All;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (ClusterCancelAllOrders* thisAsPointer = &this)
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
			if ((int)Flags >> 2 != 0)
			{
				return HftRejectReasonCode.InvalidOrderFormat;
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

		public readonly CancelAllOrdersFlags Flags;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ClusterCancelAllOrders>());
	}
}