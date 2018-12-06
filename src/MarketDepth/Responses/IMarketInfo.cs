namespace GridEx.API.MarketDepth.Responses
{
	public interface IMarketInfo : ISizeable
	{
		MarketInfoTypeCode TypeCode { get; }
	}
}