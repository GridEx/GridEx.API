using System;
using System.Runtime.CompilerServices;

namespace GridEx.API
{
	public sealed class VolumeRange
	{
		public event Action<VolumeRange> OnRangeChanged = delegate { };

		public static readonly VolumeRange Instance = new VolumeRange();

		public void Init(double buyMin, double buyMax, double sellMin, double sellMax)
		{
			if (buyMin >= buyMax)
			{
				throw new ArgumentOutOfRangeException(nameof(buyMax));
			}

			if (sellMin >= sellMax)
			{
				throw new ArgumentOutOfRangeException(nameof(sellMax));
			}

			BuyMin = buyMin;
			BuyMax = buyMax;

			SellMin = sellMin;
			SellMax = sellMax;

			OnRangeChanged(this);
		}

		public double BuyMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double BuyMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double SellMin
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double SellMax
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InBuySideRange(double volume, double price)
		{
			var buySideVolume = volume * price;

			return BuyMin <= buySideVolume && buySideVolume <= BuyMax;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InBuySideRange(double buySideVolume)
		{
			return BuyMin <= buySideVolume && buySideVolume <= BuyMax;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InSellSideRange(double sellSideVolume)
		{
			return SellMin <= sellSideVolume && sellSideVolume <= SellMax;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InSellSideRange(double sellSideVolume, double sellSidePrice)
		{
			return InSellSideRange(sellSideVolume) && InBuySideRange(sellSideVolume, sellSidePrice);
		}

		private VolumeRange()
		{
		}
	}
}