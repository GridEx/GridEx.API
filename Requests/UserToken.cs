using GridEx.API.Responses;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct UserToken : IHftRequest
	{
		public unsafe UserToken(int requestId, byte[] value)
		{
			if (value.Length != ValueSize)
			{
				throw new ArgumentException(nameof(value));
			}

			fixed (byte* source = &value[0])
			{
				ulong* p = (ulong*)source;
				_part0 = p[0];
				_part1 = p[1];
			}

			Size = MessageSize;
			TypeCode = RequestTypeCode.UserToken;
			RequestId = requestId;
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
			return RejectReasonCode.Ok;
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
			return _part0.ToString() + _part1.ToString();
		}

		private readonly ulong _part0;
		private readonly ulong _part1;
		private const int ValueSize = 16;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<UserToken>());
	}
}