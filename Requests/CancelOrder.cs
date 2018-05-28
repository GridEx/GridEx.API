using GridEx.API.Responses;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct CancelOrder : IHftRequest
	{
		public CancelOrder(int requestId, long orderId)
		{
			Size = MessageSize;
			TypeCode = RequestTypeCode.CancelOrder;
			RequestId = requestId;
			OrderId = orderId;
		}

		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (CancelOrder* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		public unsafe static ref readonly CancelOrder CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((CancelOrder*)source)[0];
			}
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public RejectReasonCode IsValid()
		{
			if (OrderId < 0)
			{
				return RejectReasonCode.OrderNotFound;
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

		public readonly long OrderId;

		public static readonly byte MessageSize = Convert.ToByte(Marshal.SizeOf<CancelOrder>());
	}
}