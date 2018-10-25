using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct HistorySettings : IHistoryResponse
	{
		public HistorySettings(byte digitsAfterPoint)
		{
			Size = MessageSize;
			TypeCode = HistoryResponseTypeCode.HistorySettings;
			DigitsAfterPoint = digitsAfterPoint;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref HistorySettings CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((HistorySettings*)source)[0];
			}
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

		public fixed char MarketName[16];
		public byte DigitsAfterPoint;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<HistorySettings>());
	}
}
