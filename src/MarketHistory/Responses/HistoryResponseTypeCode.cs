namespace GridEx.API.MarketHistory.Responses
{
	public enum HistoryResponseTypeCode : byte
	{
		RequestRejected = 1,

		LastHistory = 10,

		TickChange = 20,

		RestrictionsViolated = 100
	}
}