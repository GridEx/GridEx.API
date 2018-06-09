namespace GridEx.API.Responses
{
	public enum RejectReasonCode : byte
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

		TooManyUsersConnected = 50
	}
}
