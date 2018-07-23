using System;

namespace GridEx.API.Trading.Requests
{
	[Flags]
	public enum CancelAllOrdersFlags : byte
	{
		Buy = 1,
		Sell = Buy << 1,
		All = Buy | Sell
	}
}