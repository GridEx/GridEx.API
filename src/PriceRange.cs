using System;
using System.Runtime.CompilerServices;

namespace GridEx.API
{
	public sealed class PriceRange
	{
		public static readonly PriceRange Instance = new PriceRange();

		public event Action<PriceRange> OnRangeChanged = delegate { };

		public void Init(double min, double max)
		{
			if (min >= max)
			{
				throw new ArgumentOutOfRangeException(nameof(max));
			}

			Min = min;
			Max = max;

			OnRangeChanged(this);
		}

		public double Min
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double Max
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InRange(double price)
		{
			return Min <= price && price <= Max;
		}
	}
}