namespace GridEx.API.Trading.Requests
{
	public enum HftRequestTypeCode : byte
	{
		AccessToken = 1,
		GetUserStatus = 2,
		GetClusterUserStatus = 3,

		BuyOrder = 10,
		BuyLimitOrder = 11,
		BuyLimitOrderFoK = 12,
		BuyLimitOrderIoC = 13,

		SellOrder = 20,
		SellLimitOrder = 21,
		SellLimitOrderFoK = 22,
		SellLimitOrderIoC = 23,

		CancelOrder = 30,
		CancelAllOrders = 31,

		ClusterBuyOrder = 50,
		ClusterBuyLimitOrder = 51,
		ClusterBuyLimitOrderFoK = 52,
		ClusterBuyLimitOrderIoC = 53,

		ClusterSellOrder = 60,
		ClusterSellLimitOrder = 61,
		ClusterSellLimitOrderFoK = 62,
		ClusterSellLimitOrderIoC = 63,

		ClusterCancelOrder = 70,
		ClusterCancelAllOrders = 71,

		ClusterUserConnected = 80,
		ClusterUserDisconnected = 81
	}
}