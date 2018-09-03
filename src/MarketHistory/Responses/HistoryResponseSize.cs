using GridEx.API.Helpers;

namespace GridEx.API.MarketHistory.Responses
{
	public static class HistoryResponseSize
	{
		static HistoryResponseSize()
		{
			MessageSizeHelper.GetMinMax<IHistoryResponse>(out Min, out Max);
		}

		public static readonly int Min;

		public static readonly int Max;
	}
}