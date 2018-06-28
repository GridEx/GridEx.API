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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly ConnectionTooSlow CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((ConnectionTooSlow*)source)[0];
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

		public readonly int ResponseQueueSize;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<ConnectionTooSlow>());
	}
}
