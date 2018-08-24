using System.Runtime.CompilerServices;

namespace GridEx.API
{
	public static class PriceRange
    {
		public static double Min;

		public static double Max;

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool InRange(double price)
	    {
		    return Min <= price && price <= Max;
	    }
    }
}