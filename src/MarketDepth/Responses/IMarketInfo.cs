namespace GridEx.API.MarketDepth.Responses
{
	public interface IMarketInfo
	{
		ushort Size { get; }

		MarketInfoTypeCode TypeCode { get; }
	}
}