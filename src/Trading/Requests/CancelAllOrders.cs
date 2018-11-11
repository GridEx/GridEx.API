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
			TypeCode = HftRequestTypeCode.CancelAllOrders;
			RequestId = requestId;
			Flags = flags;
		}

		public CancelAllOrders(long requestId)
		{
			Size = MessageSize;
			TypeCode = HftRequestTypeCode.CancelAllOrders;
			RequestId = requestId;
			Flags = CancelAllOrdersFlags.All;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (CancelAllOrders* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				var source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HftRejectReasonCode IsValid()
		{
			if ((int)Flags >> 2 != 0)
			{
				return HftRejectReasonCode.InvalidOrderFormat;
			}

			return HftRejectReasonCode.Ok;
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HftRequestTypeCode TypeCode
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