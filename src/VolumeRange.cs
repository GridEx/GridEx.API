using System.Runtime.CompilerServices;

namespace GridEx.API
{
	public static class VolumeRange
    {
		public static double Min;

		public static double Max;

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool InBuySideRange(double volume, double price)
	    {
		    var buySideVolume = volume * price;

		    return Min <= buySideVolume && buySideVolume <= Max;
	    }

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool InBuySideRange(double buySideVolume)
	    {
		    return Min <= buySideVolume && buySideVolume <= Max;
	    }
    }
}