using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketStream
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct MarketSnapshot : IMarketInfo
	{
		public MarketSnapshot(long time)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.MarketSnapshot;
			Time = time;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref readonly MarketSnapshot CopyFrom(byte[] array, int offset = 0)
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

		public long Time;

		public fixed double BuyPrices[Depth];
		public fixed double BuyVolumes[Depth];

		public fixed double SellPrices[Depth];
		public fixed double SellVolumes[Depth];

		public const int Depth = 45;
		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketSnapshot>());
	}
}
