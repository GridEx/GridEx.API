using System;
using System.Runtime.CompilerServices;

namespace GridEx.API
{
	public sealed class VolumeRange
	{
		public event Action<VolumeRange> OnRangeChanged = delegate { };

		public static readonly VolumeRange Instance = new VolumeRange();

		public void Init(double bidMinVolume, double bidMaxVolume, double askMinVolume, double askMaxVolume)
		{
			if (bidMinVolume >= bidMaxVolume)
			{
				throw new ArgumentOutOfRangeException(nameof(bidMaxVolume));
			}

			if (askMinVolume >= askMaxVolume)
			{
				throw new ArgumentOutOfRangeException(nameof(askMaxVolume));
			}

			BidMinVolume = bidMinVolume;
			BidMaxVolume = bidMaxVolume;

			AskMinVolume = askMinVolume;
			AskMaxVolume = askMaxVolume;

			OnRangeChanged(this);
		}

		public double BidMinVolume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double BidMaxVolume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double AskMinVolume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		public double AskMaxVolume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			private set;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InBuySideRange(double volume, double price)
		{
			var buySideVolume = volume * price;

			return BidMinVolume <= buySideVolume && buySideVolume <= BidMaxVolume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InBuySideRange(double buySideVolume)
		{
			return BidMinVolume <= buySideVolume && buySideVolume <= BidMaxVolume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool InSellSideRange(double sellSideVolume)
		{
			return AskMinVolume <= sellSideVolume && sellSideVolume <= AskMaxVolume;
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