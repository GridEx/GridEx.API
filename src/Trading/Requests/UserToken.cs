using GridEx.API.Trading.Responses;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct UserToken : IHftRequest
	{
		// token as int64 is temporary solution for simple testing
		public UserToken(long requestId, long value)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.UserToken;
			RequestId = requestId;
			Value = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (UserToken* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RejectReasonCode IsValid()
		{
			if (Value != 0)
			{
				return RejectReasonCode.Ok;
			}

			return RejectReasonCode.InvalidUserToken;
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public RequestTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public long RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public readonly long Value;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<UserToken>());
	}
}