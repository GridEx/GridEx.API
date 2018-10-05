namespace GridEx.API.MarketHistory.Responses
{
	public enum HistoryRejectReasonCode : byte
	{
		Ok = 0,

		InvalidRequestFormat = 1,
		InvalidRequestType = 2,
		InvalidRequestLength = 3,
		
		
		InvalidApiVersion = 10,
		InvalidAccessToken = 11,

		InvalidTimeFrame = 20,
		InvalidLastBarTime = 21
	}
}