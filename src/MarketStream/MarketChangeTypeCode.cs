namespace GridEx.API.MarketStream
{
    public enum MarketChangeTypeCode : byte
	{
		BidByAddedOrder = 10,
		BidByExecutedOrder = 11,
		BidByCancelledOrder = 12,
		AskByAddedOrder = 20,
		AskByExecutedOrder = 21,
		AskByCancelledOrder = 22,
		BuyVolumeByAddedOrder = 30,
		BuyVolumeByExecutedOrder = 31,
		BuyVolumeByCancelledOrder = 32,
		SellVolumeByAddedOrder = 40,
		SellVolumeByExecutedOrder = 41,
		SellVolumeByCancelledOrder = 42,
		InfoAboutVolumeAdded = 50
	}
}
