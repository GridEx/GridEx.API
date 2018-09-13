using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketStream
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct MarketStreamRestrictionsViolated : IMarketInfo
	{
		public MarketStreamRestrictionsViolated(MarketStreamRestrictionTypeCode restriction)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.RestrictionsViolated;
			Restriction = restriction;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly MarketStreamRestrictionsViolated CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketStreamRestrictionsViolated*)source)[0];
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

		public readonly MarketStreamRestrictionTypeCode Restriction;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketStreamRestrictionsViolated>());
	}
}
