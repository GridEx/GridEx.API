using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct ConnectionTooSlow : IHftResponse
	{
		public ConnectionTooSlow(int responseQueueSize)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.ConnectionTooSlow;
			ResponseQueueSize = responseQueueSize;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (ConnectionTooSlow* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public static unsafe ref readonly ConnectionTooSlow CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((ConnectionTooSlow*)source)[0];
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

		public readonly int ResponseQueueSize;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<ConnectionTooSlow>());
	}
}
