namespace GridEx.API.Trading.Responses
{
	public enum HftResponseTypeCode : byte
    {
		MarketSettings = 2,

		OrderCreated = 10,
		OrderExecuted = 11,
		OrderCanceled = 12,
		AllOrdersCanceled = 13,

		OrderRejected = 30,

	    UserStatus = 40,
		
	    ClusterOrderCreated = 50,
	    ClusterOrderExecuted = 51,
	    ClusterOrderCanceled = 52,
	    ClusterAllOrdersCanceled = 53,

	    ClusterOrderRejected = 70,

	    ClusterUserStatus = 80,

		AccessTokenAccepted = 100,
	    AccessTokenRejected = 101,
	    RestrictionsViolated = 102,
	    RequestRejected = 103
    }
}