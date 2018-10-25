using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct HftMarketSettings : IHftResponse
	{
		public HftMarketSettings(
			byte digitsAfterPoint,
			double minPrice,
			double maxPrice,
			double minBidVolume,
			double maxBidVolume,
			double minAskVolume,
			double maxAskVolume)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.MarketSettings;
			DigitsAfterPoint = digitsAfterPoint;
			MinPrice = minPrice;
			MaxPrice = maxPrice;
			MinBidVolume = minBidVolume;
			MaxBidVolume = maxBidVolume;
			MinAskVolume = minAskVolume;
			MaxAskVolume = maxAskVolume;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref HftMarketSettings CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((HftMarketSettings*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HftResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public fixed char MarketName[10];
		public byte DigitsAfterPoint;
		public double MinPrice;
		public double MaxPrice;
		public double MinBidVolume;
		public double MaxBidVolume;
		public double MinAskVolume;
		public double MaxAskVolume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<HftMarketSettings>());
	}
}
