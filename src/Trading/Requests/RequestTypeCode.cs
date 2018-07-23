namespace GridEx.API.Trading.Requests
{
	public enum RequestTypeCode : byte
	{
		UserToken = 1,

		BuyOrder = 10,
		BuyLimitOrder = 11,

		SellOrder = 20,
		SellLimitOrder = 21,

		CancelOrder = 30,
		CancelAllOrders = 31
	}
}