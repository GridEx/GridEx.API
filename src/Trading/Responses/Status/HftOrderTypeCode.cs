using GridEx.API.Trading.Requests;

namespace GridEx.API.Trading.Responses.Status
{

	public enum HftOrderTypeCode : byte
	{
		BuyLimitOrder = HftRequestTypeCode.BuyLimitOrder,
		SellLimitOrder = HftRequestTypeCode.SellLimitOrder
	}
}