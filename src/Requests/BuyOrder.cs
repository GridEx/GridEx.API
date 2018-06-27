using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Responses;

namespace GridEx.API.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct BuyOrder : IHftRequest
	{
		public BuyOrder(long requestId, double volume)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.BuyOrder;
			RequestId = requestId;
			Volume = volume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (BuyOrder* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref readonly BuyOrder CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((BuyOrder*)source)[0];
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RejectReasonCode IsValid()
		{
			if (Volume < VolumeRange.Min || Volume > VolumeRange.Max)
			{
				return RejectReasonCode.InvalidOrderVolumeRange;
			}

			return RejectReasonCode.Ok;
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

		public readonly double Volume;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<BuyOrder>());
	}
}