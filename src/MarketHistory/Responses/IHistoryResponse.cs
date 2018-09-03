namespace GridEx.API.MarketHistory.Responses
{
	public interface IHistoryResponse
	{
		ushort Size { get; }

		HistoryResponseTypeCode TypeCode { get; }
	}
}