using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.MarketHistory.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct HistoryRestrictionsViolated : IHistoryResponse
	{
		public HistoryRestrictionsViolated(HistoryRestrictionTypeCode restriction)
		{
			Size = MessageSize;
			TypeCode = HistoryResponseTypeCode.RestrictionsViolated;
			Restriction = restriction;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly HistoryRestrictionsViolated CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((HistoryRestrictionsViolated*)source)[0];
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

		public readonly HistoryRestrictionTypeCode Restriction;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<HistoryRestrictionsViolated>());
	}
}
