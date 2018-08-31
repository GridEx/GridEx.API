namespace GridEx.API.MarketStream
{
	public interface IMarketInfo
	{
		ushort Size { get; }

		MarketInfoTypeCode TypeCode { get; }

		long Time { get; }
	}
}