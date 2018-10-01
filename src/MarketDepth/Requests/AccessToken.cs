using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GridEx.API.MarketDepth.Responses;

namespace GridEx.API.MarketDepth.Requests
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct AccessToken : IMarketInfoRequest
	{
		// token as int64 is temporary solution for simple testing
		public AccessToken(long requestId, ApiVersion apiVersion, long value)
		{
			Size = MessageSize;
			TypeCode = MarketInfoRequestTypeCode.AccessToken;
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
		public MarketInfoRejectReasonCode IsValid()
		{
			if (Value != 0)
			{
				return MarketInfoRejectReasonCode.Ok;
			}

			return MarketInfoRejectReasonCode.InvalidAccessToken;
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public MarketInfoRequestTypeCode TypeCode
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