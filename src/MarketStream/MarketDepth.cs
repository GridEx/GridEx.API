using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketStream
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct MarketDepth : IMarketInfo
	{
		public MarketDepth(long time, ushort instrumentId)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.MarketDepth;
			Time = time;
			InstrumentId = instrumentId;
		}

		public int CopyTo(byte[] array, int offset = 0)
		{
			fixed (MarketDepth* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public static ref readonly MarketDepth CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketDepth*)source)[0];
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

		public long Time;
		public ushort InstrumentId;

		public fixed double BuyPrices[Depth];
		public fixed double BuyVolumes[Depth];

		public fixed double SellPrices[Depth];
		public fixed double SellVolumes[Depth];

		public const int Depth = 45;
		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketDepth>());
	}
}
