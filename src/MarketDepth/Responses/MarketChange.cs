using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketDepth.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct MarketChange : IMarketInfo
	{
		public MarketChange(MarketChangeTypeCode marketChangeType, double price, double volume)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.MarketChange;
			MarketChangeType = marketChangeType;
			Price = price;
			Volume = volume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly MarketChange CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketChange*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public MarketInfoTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public bool IsVolumeEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			// Calculations are performed on the server with much greater accuracy,
			// double type is used only as DTO, so zero is really zero.
			get => Volume == 0;
		}

		public readonly MarketChangeTypeCode MarketChangeType;
		public readonly double Price;
		public readonly double Volume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketChange>());
	}
}
