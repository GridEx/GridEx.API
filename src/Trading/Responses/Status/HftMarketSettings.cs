using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses.Status
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct HftMarketSettings
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

		public double MinPrice;
		public double MaxPrice;
		public double MinBidVolume;
		public double MaxBidVolume;
		public double MinAskVolume;
		public double MaxAskVolume;
	}
}
