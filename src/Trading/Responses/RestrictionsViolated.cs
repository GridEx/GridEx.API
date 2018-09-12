using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct RestrictionsViolated : IHftResponse
	{
		public RestrictionsViolated(RestrictionTypeCode restriction)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.RestrictionsViolated;
			Restriction = restriction;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly RestrictionsViolated CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((RestrictionsViolated*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HftResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly RestrictionTypeCode Restriction;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<RestrictionsViolated>());
	}
}
