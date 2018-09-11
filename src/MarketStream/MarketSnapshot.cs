using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketStream
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct MarketSnapshot : IMarketInfo
	{
		public MarketSnapshot(long time, byte buyLevelsQuantity, byte sellLevelsQuantity)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.MarketSnapshot;
			Time = time;
			BuyLevelsQuantity = buyLevelsQuantity;
			SellLevelsQuantity = sellLevelsQuantity;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref MarketSnapshot CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketSnapshot*)source)[0];
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

		public byte BuyLevelsQuantity;
		public fixed double BuyPrices[Depth];
		public fixed double BuyVolumes[Depth];

		public byte SellLevelsQuantity;
		public fixed double SellPrices[Depth];
		public fixed double SellVolumes[Depth];

		public const int Depth = 45;
		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketSnapshot>());
	}
}
