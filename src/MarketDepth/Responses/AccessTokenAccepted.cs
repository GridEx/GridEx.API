using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.MarketDepth.Requests;

namespace GridEx.API.MarketDepth.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct AccessTokenAccepted : IMarketInfo
	{
		public AccessTokenAccepted(in AccessToken accessToken)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.AccessTokenAccepted;
			Token = accessToken;
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

		public MarketInfoTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly AccessToken Token;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<AccessTokenAccepted>());
	}
}
