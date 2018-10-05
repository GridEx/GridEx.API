namespace GridEx.API.MarketHistory.Responses
{
	public enum HistoryResponseTypeCode : byte
	{
		History = 10,
		LastHistory = 11,

		TickChange = 20,

		AccessTokenAccepted = 100,
		AccessTokenRejected = 101,
		RestrictionsViolated = 102,
		RequestRejected = 103
	}
}