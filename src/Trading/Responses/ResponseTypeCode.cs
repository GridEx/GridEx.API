namespace GridEx.API.Trading.Responses
{
	public enum ResponseTypeCode : byte
    {
		RequestRejected = 1,

		UserTokenAccepted = 10,

		UserTokenRejected = 20,

		OrderCreated = 30,
		OrderExecuted = 31,
		OrderCancelled = 32,
		AllOrdersCancelled = 33,

		OrderRejected = 50,

		ConnectionTooSlow = 70
    }
}