using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.MarketDepth.Requests;

namespace GridEx.API.MarketDepth.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct AccessTokenRejected : IMarketInfo
	{
		public AccessTokenRejected(in AccessToken accessToken, MarketInfoRejectReasonCode rejectCode)
		{
			Size = MessageSize;
			TypeCode = MarketInfoTypeCode.AccessTokenRejected;
			Token = accessToken;
			RejectCode = rejectCode;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly AccessTokenRejected CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((AccessTokenRejected*)source)[0];
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
		public readonly MarketInfoRejectReasonCode RejectCode;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<AccessTokenRejected>());
	}
}
