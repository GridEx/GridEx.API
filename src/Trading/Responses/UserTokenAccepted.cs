using GridEx.API.Trading.Requests;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct UserTokenAccepted : IHftResponse
	{
		public UserTokenAccepted(in UserToken userToken)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.UserTokenAccepted;
			Token = userToken;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly UserTokenAccepted CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((UserTokenAccepted*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public ResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly UserToken Token;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<UserTokenAccepted>());
	}
}
