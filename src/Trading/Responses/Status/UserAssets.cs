using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses.Status
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct UserAssets
	{
		public UserAssets(
			double availableBuyVolume, 
			double buyVolumeInMarket, 
			double availableSellVolume, 
			double sellVolumeInMarket)
		{
			AvailableBuyVolume = availableBuyVolume;
			BuyVolumeInMarket = buyVolumeInMarket;
			AvailableSellVolume = availableSellVolume;
			SellVolumeInMarket = sellVolumeInMarket;
		}

		public double AvailableBuyVolume;
		public double BuyVolumeInMarket;
		public double AvailableSellVolume;
		public double SellVolumeInMarket;
	}
}