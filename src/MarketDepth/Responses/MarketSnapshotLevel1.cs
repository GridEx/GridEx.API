using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketDepth.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct MarketSnapshotLevel1 : IMarketInfo
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref MarketSnapshotLevel1 CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketSnapshotLevel1*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public MarketInfoTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public long Time
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public double BidPrice;
		public double BidVolume;
		public double AskPrice;
		public double AskVolume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketSnapshotLevel1>());
	}
}
