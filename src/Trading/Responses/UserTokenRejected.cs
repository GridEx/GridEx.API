using GridEx.API.Trading.Requests;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct UserTokenRejected : IHftResponse
	{
		public UserTokenRejected(in UserToken userToken, HftRejectReasonCode rejectCode)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.UserTokenRejected;
			Token = userToken;
			RejectCode = rejectCode;
		}
		
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly UserTokenRejected CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((UserTokenRejected*)source)[0];
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

		public readonly UserToken Token;
		public readonly HftRejectReasonCode RejectCode;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<UserTokenRejected>());
	}
}
