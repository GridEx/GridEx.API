namespace GridEx.API
{
	public readonly struct ApiVersion
	{
		public ApiVersion(byte major, byte minor, byte patch)
		{
			Major = major;
			Minor = minor;
			Patch = patch;
		}

		public readonly byte Major;

		public readonly byte Minor;

		public readonly byte Patch;
	}
}