using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct History : IHistoryResponse
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref History CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((History*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public HistoryResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}
		
		public long RequestId;

		public ushort TimeFrame;

		public byte BarsQuantity;

		public fixed long Time[HistoryLength];

		public fixed double Open[HistoryLength];

		public fixed double High[HistoryLength];

		public fixed double Low[HistoryLength];

		public fixed double Close[HistoryLength];

		public fixed double Volume[HistoryLength];

		public const int HistoryLength = 60;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<History>());
	}
}