namespace GridEx.API.MarketHistory.Responses
{
	public enum HistoryRejectReasonCode : byte
	{
		Ok = 0,

		InvalidRequestType = 1,
		InvalidRequestLength = 2,
		InvalidRequestFormat = 3,
		
		InvalidAccessToken = 10,

		InvalidTimeFrame = 20
	}
}