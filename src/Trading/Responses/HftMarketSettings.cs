using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct HftMarketSettings : IHftResponse
	{
		public HftMarketSettings(
			double minPrice,
			double maxPrice,
			double minBidVolume,
			double maxBidVolume,
			double minAskVolume,
			double maxAskVolume)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.MarketSettings;
			MinPrice = minPrice;
			MaxPrice = maxPrice;
			MinBidVolume = minBidVolume;
			MaxBidVolume = maxBidVolume;
			MinAskVolume = minAskVolume;
			MaxAskVolume = maxAskVolume;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref HftMarketSettings CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((HftMarketSettings*)source)[0];
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

		public readonly double MinPrice;
		public readonly double MaxPrice;
		public readonly double MinBidVolume;
		public readonly double MaxBidVolume;
		public readonly double MinAskVolume;
		public readonly double MaxAskVolume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<HftMarketSettings>());
	}
}
