namespace GridEx.API.MarketHistory.Responses
{
	public enum HistoryResponseTypeCode : byte
	{
		HistorySettings = 2,

		HistoryStatus = 11,
		History = 12,
		LastHistory = 13,

		TickChange = 20,

		AccessTokenAccepted = 100,
		AccessTokenRejected = 101,
		RestrictionsViolated = 102,
		RequestRejected = 103
	}
}