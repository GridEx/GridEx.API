namespace GridEx.API.Trading.Responses
{
	public enum HftRejectReasonCode : byte
	{
		Ok = 0,

		InvalidRequestFormat = 1,
		InvalidRequestType = 2,
		InvalidRequestLength = 3,
		
		InvalidApiVersion = 10,
		InvalidAccessToken = 11,

		InvalidOrderFormat = 20,
		InvalidOrderPriceRange = 21,
		InvalidOrderVolumeRange = 22,
		InvalidUserId = 23,

		TooManyLimitOrdersCreatedByUser = 30,
		InsufficientFunds = 31,
		OrderNotFound = 32,
		InsufficientVolumeInMarket = 33
	}
}
