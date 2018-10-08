namespace GridEx.API.MarketHistory.Responses
{
	public enum HistoryResponseTypeCode : byte
	{
		HistoryStatus = 10,
		History = 11,
		LastHistory = 12,

		TickChange = 20,

		AccessTokenAccepted = 100,
		AccessTokenRejected = 101,
		RestrictionsViolated = 102,
		RequestRejected = 103
	}
}