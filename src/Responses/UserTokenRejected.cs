using GridEx.API.Requests;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct UserTokenRejected : IHftResponse
	{
		public UserTokenRejected(in UserToken userToken, RejectReasonCode rejectCode)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.UserTokenRejected;
			Token = userToken;
			RejectCode = rejectCode;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (UserTokenRejected* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public static unsafe ref readonly UserTokenRejected CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((UserTokenRejected*)source)[0];
			}
		}

		public byte Size
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
		public readonly RejectReasonCode RejectCode;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<UserTokenRejected>());
	}
}
