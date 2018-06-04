using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct AllOrdersCancelled : IHftResponse
	{
		public AllOrdersCancelled(long requestId, byte amount)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.AllOrdersCancelled;
			RequestId = requestId;
			Amount = amount;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (AllOrdersCancelled* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public static unsafe ref readonly AllOrdersCancelled CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((AllOrdersCancelled*)source)[0];
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

		public long RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public readonly byte Amount;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<AllOrdersCancelled>());
	}
}
