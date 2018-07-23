using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.Trading.Responses;

namespace GridEx.API.Trading.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct CancelAllOrders : IHftRequest
	{
		public CancelAllOrders(long requestId, CancelAllOrdersFlags flags)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.CancelAllOrders;
			RequestId = requestId;
			Flags = flags;
		}

		public CancelAllOrders(long requestId)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.CancelAllOrders;
			RequestId = requestId;
			Flags = CancelAllOrdersFlags.Buy | CancelAllOrdersFlags.Sell;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (CancelAllOrders* thisAsPointer = &this)
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
			if ((int)Flags >> 2 != 0)
			{
				return RejectReasonCode.InvalidOrderFormat;
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

		public readonly CancelAllOrdersFlags Flags;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<CancelAllOrders>());
	}
}