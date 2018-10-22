using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading.Requests.Cluster
{
	public readonly struct ClusterUserConnected : IClusterHftRequest
	{
		public ClusterUserConnected(long requestId, long userId)
		{
			Size = MessageSize;
			TypeCode = HftRequestTypeCode.ClusterUserConnected;
			RequestId = requestId;
			UserId = userId;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (ClusterUserConnected* thisAsPointer = &this)
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

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ClusterUserConnected>());
	}
}
