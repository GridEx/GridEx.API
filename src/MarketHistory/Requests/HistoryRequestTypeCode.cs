namespace GridEx.API.MarketHistory.Requests
{
	public enum HistoryRequestTypeCode : byte
	{
		AccessToken = 1,

		GetHistoryStatus = 10,
		GetHistoryRequest = 11,
		GetLastHistoryRequest = 12,

		GetHistoryStatusRequest = 20
	}
}