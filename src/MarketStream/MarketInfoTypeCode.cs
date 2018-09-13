namespace GridEx.API.MarketStream
{
	public enum MarketInfoTypeCode : byte
	{
		MarketSnapshot = 10,

		MarketChange = 20,

		RestrictionsViolated = 100
	}
}