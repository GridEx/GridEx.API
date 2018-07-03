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
		VolumeByAddedOrder = 30,
		VolumeByExecutedOrder = 31,
		VolumeByCancelledOrder = 32,
		InfoAboutVolumeAdded = 40
	}
}
