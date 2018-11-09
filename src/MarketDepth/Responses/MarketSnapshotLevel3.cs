using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketDepth.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct MarketSnapshotLevel3 : IMarketInfo
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref MarketSnapshotLevel3 CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketSnapshotLevel3*)source)[0];
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

		public byte BidLevelsQuantity;
		public fixed double BidPrices[MaxDepth];
		public fixed double BidVolumes[MaxDepth];

		public byte AskLevelsQuantity;
		public fixed double AskPrices[MaxDepth];
		public fixed double AskVolumes[MaxDepth];

		public const int MaxDepth = 45;
		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketSnapshotLevel3>());
	}
}
