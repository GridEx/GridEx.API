using System;
using System.Runtime.CompilerServices;

namespace GridEx.API.MarketHistory
{
	public static class SupportedTimeFrames
	{
		public static readonly ushort[] Values = 
		{
			1,
			2,
			3,
			4,
			5,
			6,
			7,
			8,
			9,
			10,
			15,
			20,
			30,
			60,
			120,
			180,
			240,
			360,
			480,
			720,
			1440,
			10080
		};

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValid(ushort timeFrame)
		{
			return Array.BinarySearch(Values, timeFrame) >= 0;
		}
	}
}