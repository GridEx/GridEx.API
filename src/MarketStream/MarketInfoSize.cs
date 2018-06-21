using GridEx.API.Helpers;

namespace GridEx.API.MarketStream
{
	public static class MarketInfoSize
	{
		static MarketInfoSize()
		{
			MessageSizeHelper.GetMinMax<IMarketInfo>(out Min, out Max);
		}

		public static readonly int Min;

		public static readonly int Max;
	}
}