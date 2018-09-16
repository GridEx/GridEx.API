namespace GridEx.API.Trading.Responses
{
	public enum HftResponseTypeCode : byte
    {
		OrderCreated = 10,
		OrderExecuted = 11,
		OrderCanceled = 12,
		AllOrdersCanceled = 13,

		OrderRejected = 30,

	    CurrentStatus = 40,

		AccessTokenAccepted = 100,
	    AccessTokenRejected = 101,
	    RestrictionsViolated = 102,
	    RequestRejected = 103
    }
}