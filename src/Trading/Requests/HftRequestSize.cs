using GridEx.API.Helpers;

namespace GridEx.API.Trading.Requests
{
	public static class HftRequestSize
	{
		static HftRequestSize()
		{
			MessageSizeHelper.GetMinMax<IHftRequest>(out Min, out Max);
		}

		public static readonly int Min;

		public static readonly int Max;
	}
}