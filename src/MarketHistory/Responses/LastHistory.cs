using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct LastHistory : IHistoryResponse
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref LastHistory CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((LastHistory*)source)[0];
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

		public long LastChangeTime;

		public ushort TimeFrame;

		public fixed long Time[LastHistoryLength];

		public fixed double Open[LastHistoryLength];

		public fixed double High[LastHistoryLength];

		public fixed double Low[LastHistoryLength];

		public fixed double Close[LastHistoryLength];

		public fixed double Volume[LastHistoryLength];

		public const int LastHistoryLength = 2;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<LastHistory>());
	}
}