using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading.Requests.Cluster
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct ClusterCancelOrder : IClusterHftRequest
	{
		public ClusterCancelOrder(long requestId, long userId, long orderId)
		{
			Size = MessageSize;
			TypeCode = HftRequestTypeCode.ClusterCancelOrder;
			RequestId = requestId;
			UserId = userId;
			OrderId = orderId;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (ClusterCancelOrder* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				var source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HftRejectReasonCode IsValid()
		{
			if (OrderId == 0)
			{
				return HftRejectReasonCode.OrderNotFound;
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

		public readonly long OrderId;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ClusterCancelOrder>());
	}
}