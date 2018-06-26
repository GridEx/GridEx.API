using GridEx.API.Helpers;

namespace GridEx.API.Requests
{
	public static class RequestSize
	{
		static RequestSize()
		{
			MessageSizeHelper.GetMinMax<IHftRequest>(out Min, out Max);
		}

		public static readonly int Min;

		public static readonly int Max;
	}
}