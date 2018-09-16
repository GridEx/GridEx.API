using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketDepth.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct MarketDepthRestrictionsViolated : IMarketInfo
	{
		public MarketDepthRestrictionsViolated(MarketDepthRestrictionTypeCode restriction)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.RestrictionsViolated;
			Restriction = restriction;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly MarketDepthRestrictionsViolated CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketDepthRestrictionsViolated*)source)[0];
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

		public readonly MarketDepthRestrictionTypeCode Restriction;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<MarketDepthRestrictionsViolated>());
	}
}
