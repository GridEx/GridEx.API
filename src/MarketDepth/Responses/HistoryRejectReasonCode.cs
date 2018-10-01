namespace GridEx.API.MarketDepth.Responses
{
	public enum MarketInfoRejectReasonCode : byte
	{
		Ok = 0,

		InvalidRequestType = 1,
		InvalidRequestLength = 2,
		InvalidRequestFormat = 3,
		
		InvalidApiVersion = 10,
		InvalidAccessToken = 11
	}
}