using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct TickChange : IHistoryResponse
	{
		public TickChange(long time, double price, double volume)
		{
			Size = MessageSize;
			TypeCode = HistoryResponseTypeCode.TickChange;
			Time = time;
			Price = price;
			Volume = volume;
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HistoryResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly TickChange CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((TickChange*)source)[0];
			}
		}

		public readonly long Time;

		public readonly double Price;

		public readonly double Volume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<TickChange>());
	}
}
