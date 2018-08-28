using System;
using System.Runtime.CompilerServices;

namespace GridEx.API
{
	public static class VolumeRange
	{
		public static event Action<double, double> OnRangeChanged = delegate { };

		public static void Init(double min, double max)
		{
			if (min >= max)
			{
				throw new ArgumentOutOfRangeException(nameof(max));
			}

			Min = min;
			Max = max;

			OnRangeChanged(Min, Max);
		}

		public static double Min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public static double Max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

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