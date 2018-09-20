using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses.Status
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct HftMarketSettings
	{
		public HftMarketSettings(
			double minPrice, 
			double maxPrice, 
			double minBidVolume, 
			double maxBidVolume, 
			double minAskVolume, 
			double maxAskVolume)
		{
			MinPrice = minPrice;
			MaxPrice = maxPrice;
			MinBidVolume = minBidVolume;
			MaxBidVolume = maxBidVolume;
			MinAskVolume = minAskVolume;
			MaxAskVolume = maxAskVolume;
		}

		public readonly double MinPrice;
		public readonly double MaxPrice;
		public readonly double MinBidVolume;
		public readonly double MaxBidVolume;
		public readonly double MinAskVolume;
		public readonly double MaxAskVolume;
	}
}
