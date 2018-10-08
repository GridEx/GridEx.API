namespace GridEx.API.MarketHistory.Requests
{
	public enum HistoryRequestTypeCode : byte
	{
		AccessToken = 1,

		HistoryStatus = 10,
		GetHistoryRequest = 11,
		GetLastHistoryRequest = 12,

		GetHistoryStatusRequest = 20
	}
}