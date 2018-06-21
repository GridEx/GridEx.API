using System.Linq;
using System.Reflection;

namespace GridEx.API.Helpers
{
    internal static class MessageSizeHelper
    {
	    public static void GetMinMax<T>(out int minSize, out int maxSize)
	    {
		    var responseSizes = typeof(T)
			    .Assembly
			    .GetTypes()
			    .Where(type => typeof(T).IsAssignableFrom(type) && type.IsValueType)
			    .Select(type => type.GetField("MessageSize", BindingFlags.Public | BindingFlags.Static))
			    .Select(field => (ushort)field.GetValue(null));

		    minSize = responseSizes.Min();

		    maxSize = responseSizes.Max();
	    }
	}
}