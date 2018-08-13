namespace GridEx.API.Trading.Requests
{
	public enum RequestTypeCode : byte
	{
		UserToken = 1,

		BuyOrder = 10,
		BuyLimitOrder = 11,
		BuyLimitOrderFoK = 12,
		BuyLimitOrderIoC = 13,

		SellOrder = 20,
		SellLimitOrder = 21,
		SellLimitOrderFoK = 22,
		SellLimitOrderIoC = 23,

		CancelOrder = 30,
		CancelAllOrders = 31
	}
}