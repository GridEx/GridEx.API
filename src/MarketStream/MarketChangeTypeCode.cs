namespace GridEx.API.MarketStream
{
    public enum MarketChangeTypeCode : byte
	{
		BidByAddedOrder = 10,
		BidByExecutedOrder = 11,
		BidByCanceledOrder = 12,
		AskByAddedOrder = 20,
		AskByExecutedOrder = 21,
		AskByCanceledOrder = 22,
		BuyVolumeByAddedOrder = 30,
		BidVolumeByExecutedOrder = 31,
		BuyVolumeByCanceledOrder = 32,
		SellVolumeByAddedOrder = 40,
		AskVolumeByExecutedOrder = 41,
		SellVolumeByCanceledOrder = 42,
		BuyVolumeInfoAdded = 50,
		SellVolumeInfoAdded = 51
	}
}
