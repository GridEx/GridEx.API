using GridEx.API.Requests;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
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

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (UserTokenAccepted* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public unsafe static ref readonly UserTokenAccepted CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((UserTokenAccepted*)source)[0];
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

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<UserTokenAccepted>());
	}
}
