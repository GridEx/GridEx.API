using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses.Status
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct CurrentStatus : IHftResponse
	{
		public CurrentStatus(
			ref HftMarketSettings marketSettings,
			ref UserAssets userAssets,
			ref UserLiveOrders userLiveOrders)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.CurrentStatus;
			MarketSettings = marketSettings;
			UserAssets = userAssets;
			UserLiveOrders = userLiveOrders;
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref CurrentStatus CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((CurrentStatus*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HftResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
		}

		public HftMarketSettings MarketSettings;

		public UserAssets UserAssets;

		public UserLiveOrders UserLiveOrders;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<CurrentStatus>());
	}
}