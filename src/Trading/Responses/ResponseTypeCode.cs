namespace GridEx.API.Trading.Responses
{
	public enum ResponseTypeCode : byte
    {
		RequestRejected = 1,

		UserTokenAccepted = 10,

		UserTokenRejected = 20,

		OrderCreated = 30,
		OrderExecuted = 31,
		OrderCanceled = 32,
		AllOrdersCanceled = 33,

		OrderRejected = 50,

		ConnectionTooSlow = 70
    }
}