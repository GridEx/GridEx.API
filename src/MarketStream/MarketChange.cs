using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketStream
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct MarketChange : IHftMarketInfo
	{
		public MarketChange(double price, double volume)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.MarketChange;
			Price = price;
			Volume = volume;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (MarketChange* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

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

		public bool IsEmpty
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			// Calculations are performed on the server with much greater accuracy,
			// double type is used only as DTO, so zero is really zero.
			get { return Volume == 0; }
		}

		public readonly double Price;
		public readonly double Volume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketChange>());
	}
}
