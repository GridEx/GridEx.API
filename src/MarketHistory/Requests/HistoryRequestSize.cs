using GridEx.API.Helpers;

namespace GridEx.API.MarketHistory.Requests
{
	public static class HistoryRequestSize
	{
		static HistoryRequestSize()
		{
			MessageSizeHelper.GetMinMax<IHistoryRequest>(out Min, out Max);
		}

		public static readonly int Min;

		public static readonly int Max;
	}
}