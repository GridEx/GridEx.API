namespace GridEx.API.Trading.Responses
{
	public enum HftRejectReasonCode : byte
	{
		Ok = 0,

		InvalidRequestType = 1,
		InvalidRequestLength = 2,
		InvalidRequestFormat = 3,

		InvalidUserToken = 10,

		InvalidOrderFormat = 20,
		InvalidOrderPriceRange = 21,
		InvalidOrderVolumeRange = 22,

		TooManyOrdersCreatedByUser = 30,
		InsufficientFunds = 31,
		OrderNotFound = 32,
		InsufficientVolumeInMarket = 33,

		TooManyUsersConnected = 50
	}
}
