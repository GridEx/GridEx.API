using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses.Cluster
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct ClusterAllOrdersCanceled : IClusterHftResponse
	{
		public ClusterAllOrdersCanceled(long requestId, long userId, byte quantity)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.ClusterAllOrdersCanceled;
			RequestId = requestId;
			UserId = userId;
			Quantity = quantity;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly ClusterAllOrdersCanceled CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((ClusterAllOrdersCanceled*)source)[0];
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

		public readonly byte Quantity;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ClusterAllOrdersCanceled>());
	}
}
