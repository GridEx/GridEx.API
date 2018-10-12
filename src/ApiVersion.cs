using System.Runtime.InteropServices;

namespace GridEx.API
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct ApiVersion
	{
		public ApiVersion(ushort major, ushort minor, ushort patch)
		{
			Major = major;
			Minor = minor;
			Patch = patch;
		}

		public readonly ushort Major;

		public readonly ushort Minor;

		public readonly ushort Patch;
	}
}