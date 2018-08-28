using System;
using System.Runtime.CompilerServices;

namespace GridEx.API
{
	public static class PriceRange
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
		public static bool InRange(double price)
		{
			return Min <= price && price <= Max;
		}
	}
}