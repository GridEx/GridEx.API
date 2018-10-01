namespace GridEx.API.MarketHistory.Responses
{
	public enum HistoryRejectReasonCode : byte
	{
		Ok = 0,

		InvalidRequestType = 1,
		InvalidRequestLength = 2,
		InvalidRequestFormat = 3,
		
		InvalidApiVersion = 10,
		InvalidAccessToken = 11,

		InvalidTimeFrame = 20
	}
}