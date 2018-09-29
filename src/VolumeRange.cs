using System;
using System.Runtime.CompilerServices;

namespace GridEx.API
{
	public sealed class VolumeRange
	{
		public event Action<VolumeRange> OnRangeChanged;

		public static readonly VolumeRange Instance = new VolumeRange();

		public void Init(double minBidVolume, double maxBidVolume, double minAskVolume, double maxAskVolume)
		{
			if (minBidVolume >= maxBidVolume)
			{
				throw new ArgumentOutOfRangeException(nameof(maxBidVolume));
			}

			if (minAskVolume >= maxAskVolume)
			{
				throw new ArgumentOutOfRangeException(nameof(maxAskVolume));
			}

			MinBidVolume = minBidVolume;
			MaxBidVolume = maxBidVolume;

			MinAskVolume = minAskVolume;
			MaxAskVolume = maxAskVolume;

			OnRangeChanged?.Invoke(this);
		}

		public double MinBidVolume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double MaxBidVolume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double MinAskVolume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double MaxAskVolume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InBuySideRange(double volume, double price)
		{
			var buySideVolume = volume * price;

			return MinBidVolume <= buySideVolume && buySideVolume <= MaxBidVolume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InBuySideRange(double buySideVolume)
		{
			return MinBidVolume <= buySideVolume && buySideVolume <= MaxBidVolume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InSellSideRange(double sellSideVolume)
		{
			return MinAskVolume <= sellSideVolume && sellSideVolume <= MaxAskVolume;
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