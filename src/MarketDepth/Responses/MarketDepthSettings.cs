using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketDepth.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct MarketDepthSettings : IMarketInfo
	{
		public MarketDepthSettings(byte digitsAfterPoint)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.MarketDepthSettings;
			DigitsAfterPoint = digitsAfterPoint;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref MarketDepthSettings CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketDepthSettings*)source)[0];
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

		public fixed char MarketName[10];
		public byte DigitsAfterPoint;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketDepthSettings>());
	}
}
