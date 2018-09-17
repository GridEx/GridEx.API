namespace GridEx.API.MarketDepth.Responses
{
	public enum MarketInfoTypeCode : byte
	{
		MarketSnapshotLevel1 = 10,
		MarketSnapshotLevel2 = 11,
		MarketSnapshotLevel3 = 12,

		MarketChange = 20,

		AccessTokenAccepted = 100,
		AccessTokenRejected = 101,
		RestrictionsViolated = 102
	}
}