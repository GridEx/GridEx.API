using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Responses
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct MarketInfo : IHftResponse
	{
		public MarketInfo(long requestId, long time)
		{
			Size = MessageSize;
			TypeCode = ResponseTypeCode.MarketInfo;
			RequestId = requestId;
			Time = time;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (MarketInfo* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public unsafe static ref readonly MarketInfo CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((MarketInfo*)source)[0];
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

		public readonly long RequestId;
		public readonly long Time;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<MarketInfo>());
	}
}
