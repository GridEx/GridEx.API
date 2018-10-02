namespace GridEx.API.MarketDepth.Responses
{
	public enum MarketInfoRejectReasonCode : byte
	{
		Ok = 0,

		InvalidRequestFormat = 1,
		InvalidRequestType = 2,
		InvalidRequestLength = 3,
		
		InvalidApiVersion = 10,
		InvalidAccessToken = 11
	}
}