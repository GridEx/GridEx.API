using GridEx.API.Trading.Responses;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct SellOrder : IHftOrder
	{
		public SellOrder(long requestId, double volume)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.SellOrder;
			RequestId = requestId;
			Volume = volume;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (SellOrder* thisAsPointer = &this)
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

		public double Volume
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<SellOrder>());
	}
}