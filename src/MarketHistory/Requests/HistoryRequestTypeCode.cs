namespace GridEx.API.MarketHistory.Requests
{
	public enum HistoryRequestTypeCode : byte
	{
		AccessToken = 1,
		
		GetHistoryStatus = 10,
		GetHistory = 11,
		GetLastHistory = 12
	}
}