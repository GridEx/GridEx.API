using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.MarketHistory.Responses;

namespace GridEx.API.MarketHistory.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct AccessToken : IHistoryRequest
	{
		// token as int64 is temporary solution for simple testing
		public AccessToken(long requestId, ApiVersion apiVersion, long value)
		{
			Size = MessageSize;
			TypeCode = HistoryRequestTypeCode.AccessToken;
			RequestId = requestId;
			ApiVersion = apiVersion;
			Value = value;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe int CopyTo(byte[] array, int offset = 0)
		{
			fixed (AccessToken* thisAsPointer = &this)
			fixed (byte* target = &array[offset])
			{
				byte* source = (byte*)thisAsPointer;
				Buffer.MemoryCopy(source, target, MessageSize, MessageSize);
			}

			return MessageSize;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public HistoryRejectReasonCode IsValid()
		{
			if (Value != 0)
			{
				return HistoryRejectReasonCode.Ok;
			}

			return HistoryRejectReasonCode.InvalidAccessToken;
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HistoryRequestTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public long RequestId
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public override string ToString()
		{
			return Value.ToString();
		}

		public readonly ApiVersion ApiVersion;

		public readonly long Value;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<AccessToken>());
	}
}