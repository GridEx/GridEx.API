using System.Linq;
using System.Reflection;

namespace GridEx.API.Requests
{
	public static class RequestSize
	{
		static RequestSize()
		{
			var requestSizes = typeof(IHftRequest)
				.Assembly
				.GetTypes()
				.Where(type => typeof(IHftRequest).IsAssignableFrom(type) && type.IsValueType)
				.Select(type => type.GetField("MessageSize", BindingFlags.Public | BindingFlags.Static))
				.Select(field => (byte)field.GetValue(null));

			Min = requestSizes.Min();

			Max = requestSizes.Max();
		}

		public static readonly int Min;

		public static readonly int Max;
	}
}
