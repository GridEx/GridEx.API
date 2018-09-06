using System;
using System.Runtime.CompilerServices;

namespace GridEx.API.MarketHistory
{
	public static class SupportedTimeFrames
	{
		public static readonly ushort[] Values = 
		{
			1,
			3,
			5,
			10,
			15,
			30,
			60,
			120,
			240,
			480,
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