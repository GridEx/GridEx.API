using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketDepth.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct MarketSnapshotLevel2 : IMarketInfo
	{
		public MarketSnapshotLevel2(long time, byte bidLevelsQuantity, byte askLevelsQuantity)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.MarketSnapshotLevel2;
			Time = time;
			BidLevelsQuantity = bidLevelsQuantity;
			AskLevelsQuantity = askLevelsQuantity;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref MarketSnapshotLevel2 CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketSnapshotLevel2*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public MarketInfoTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
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

		public const int MaxDepth = 15;
		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketSnapshotLevel2>());
	}
}
