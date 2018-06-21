namespace GridEx.API.MarketStream
{
	public interface IHftMarketInfo
    {
		ushort Size { get; }

		MarketInfoTypeCode TypeCode { get; }

		int CopyTo(byte[] buffer, int offset = 0);
	}
}