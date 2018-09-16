namespace GridEx.API.MarketHistory.Responses
{
	public enum HistoryResponseTypeCode : byte
	{
		LastHistory = 10,

		TickChange = 20,

		AccessTokenAccepted = 100,
		AccessTokenRejected = 101,
		RestrictionsViolated = 102,
		RequestRejected = 103
	}
}