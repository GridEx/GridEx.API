using System.Runtime.CompilerServices;

namespace GridEx.API
{
	public static class VolumeRange
    {
		public static double Min;

		public static double Max;

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool InRange(double volume, double price)
	    {
		    var volumeInBuySide = volume * price;

		    return Min <= volumeInBuySide && volumeInBuySide <= Max;
	    }

	    [MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool InRange(double volumeInBuySide)
	    {
		    return Min <= volumeInBuySide && volumeInBuySide <= Max;
	    }
    }
}