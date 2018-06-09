using System.Linq;
using System.Reflection;

namespace GridEx.API.Responses
{
	public static class ResponseSize
	{
		static ResponseSize()
		{
			var responseSizes = typeof(IHftResponse)
				.Assembly
				.GetTypes()
				.Where(type => typeof(IHftResponse).IsAssignableFrom(type) && type.IsValueType)
				.Select(type => type.GetField("MessageSize", BindingFlags.Public | BindingFlags.Static))
				.Select(field => (byte)field.GetValue(null));

			Min = responseSizes.Min();

			Max = responseSizes.Max();
		}

		public static readonly int Min;

		public static readonly int Max;
	}
}
