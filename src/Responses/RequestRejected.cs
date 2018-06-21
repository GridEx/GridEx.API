using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct RequestRejected : IHftResponse
	{
		public RequestRejected(RejectReasonCode rejectCode)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.RequestRejected;
			RejectCode = rejectCode;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (RequestRejected* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public static unsafe ref readonly RequestRejected CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((RequestRejected*)source)[0];
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

		public readonly RejectReasonCode RejectCode;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<RequestRejected>());
	}
}
