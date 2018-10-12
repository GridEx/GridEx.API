using System.Runtime.InteropServices;

namespace GridEx.API
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
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