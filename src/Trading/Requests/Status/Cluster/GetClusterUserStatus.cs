using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Requests.Cluster;
using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading.Requests.Status.Cluster
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct GetClusterUserStatus : IClusterHftRequest
	{
		public GetClusterUserStatus(long requestId, long userId)
		{
			Size = MessageSize;
			TypeCode = HftRequestTypeCode.GetClusterUserStatus;
			RequestId = requestId;
			UserId = userId;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (GetClusterUserStatus* thisAsPointer = &this)
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
		
		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<GetClusterUserStatus>());
	}
}