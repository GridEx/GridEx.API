namespace GridEx.API.MarketHistory.Responses
{
	public interface IHistoryResponse : ISizeable
	{
		HistoryResponseTypeCode TypeCode { get; }
	}
}