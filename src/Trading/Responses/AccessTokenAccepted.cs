using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct AccessTokenAccepted : IHftResponse
	{
		public AccessTokenAccepted(ApiVersion apiVersion)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.AccessTokenAccepted;
			ApiVersion = apiVersion;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly AccessTokenAccepted CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((AccessTokenAccepted*)source)[0];
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

		public readonly ApiVersion ApiVersion;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<AccessTokenAccepted>());
	}
}
