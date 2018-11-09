using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Responses.Cluster;

namespace GridEx.API.Trading.Responses.Status.Cluster
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct ClusterUserCurrentStatus : IClusterHftResponse
	{
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
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public HftResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public long UserId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public double GridExTradeFee;

		public UserAssets UserAssets;

		public UserLiveOrders UserLiveOrders;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ClusterUserCurrentStatus>());
	}
}