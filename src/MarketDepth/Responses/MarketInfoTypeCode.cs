namespace GridEx.API.MarketDepth
{
	public enum MarketInfoTypeCode : byte
	{
		MarketSnapshot = 10,

		MarketChange = 20,

		AccessTokenAccepted = 100,
		AccessTokenRejected = 101,
		RestrictionsViolated = 102
	}
}