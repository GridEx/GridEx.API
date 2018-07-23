using GridEx.API.Helpers;

namespace GridEx.API.Trading.Responses
{
	public static class ResponseSize
	{
		static ResponseSize()
		{
			MessageSizeHelper.GetMinMax<IHftResponse>(out Min, out Max);
		}

		public static readonly int Min;

		public static readonly int Max;
	}
}