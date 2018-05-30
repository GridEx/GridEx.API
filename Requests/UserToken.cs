using GridEx.API.Responses;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct UserToken : IHftRequest
	{
		// token as int64 is temporary solution for simple testing
		public unsafe UserToken(int requestId, long value)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.UserToken;
			RequestId = requestId;
			Value = value;
		}

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

		public unsafe static ref readonly UserToken CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((UserToken*)source)[0];
			}
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

		public byte Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public RequestTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public int RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public readonly long Value;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<UserToken>());
	}
}