using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Responses;

namespace GridEx.API.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct BuyLimitOrder : IHftRequest
	{
		public BuyLimitOrder(int requestId, double price, double volume)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.BuyLimitOrder;
			RequestId = requestId;
			Price = price;
			Volume = volume;
		}

		public unsafe int CopyTo(byte[] buffer, int offset = 0)
		{
			fixed (BuyLimitOrder* thisAsPointer = &this)
			fixed (byte* target = &buffer[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public unsafe static ref readonly BuyLimitOrder CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((BuyLimitOrder*)source)[0];
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RejectReasonCode IsValid()
		{
			if (Price < PriceRange.Min || Price > PriceRange.Max)
			{
				return RejectReasonCode.InvalidOrderPriceRange;
			}

			if (Volume < VolumeRange.Min || Volume > VolumeRange.Max)
			{
				return RejectReasonCode.InvalidOrderVolumeRange;
			}

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

		public readonly double Price;
		public readonly double Volume;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<BuyLimitOrder>());
	}
}