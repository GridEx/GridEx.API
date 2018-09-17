namespace GridEx.API.MarketDepth.Responses
{
    public enum MarketChangeTypeCode : byte
	{
		BidPriceByAddedOrder = 10,			// Level I, Level II, Level III
		BidPriceByExecutedOrder = 11,		// Level I, Level II, Level III
		BidPriceByCanceledOrder = 12,		// Level I, Level II, Level III
		BidVolumeByExecutedOrder = 13,		// Level I, Level II, Level III
		BidVolumeByAddedOrder = 14,			// Level I, Level II, Level III
		BidVolumeByCanceledOrder = 15,		// Level I, Level II, Level III

		AskPriceByAddedOrder = 20,			// Level I, Level II, Level III
		AskPriceByExecutedOrder = 21,		// Level I, Level II, Level III
		AskPriceByCanceledOrder = 22,		// Level I, Level II, Level III
		AskVolumeByExecutedOrder = 23,		// Level I, Level II, Level III
		AskVolumeByAddedOrder = 24,			// Level I, Level II, Level III
		AskVolumeByCanceledOrder = 25,		// Level I, Level II, Level III

		BuyingVolumeByAddedOrder = 30,		// Level II, Level III
		BuyingVolumeByCanceledOrder = 31,	// Level II, Level III

		SellingVolumeByAddedOrder = 40,		// Level II, Level III
		SellingVolumeByCanceledOrder = 41,	// Level II, Level III

		BuyingVolumeInfoAdded = 50,			// Level II, Level III
		SellingVolumeInfoAdded = 51			// Level II, Level III
	}
}