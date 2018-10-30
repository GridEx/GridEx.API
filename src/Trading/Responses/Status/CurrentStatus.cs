using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses.Status
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public readonly struct CurrentStatus : IHftResponse
	{
		public CurrentStatus(
			double gridExTradeFee,
			ref UserAssets userAssets,
			ref UserLiveOrders userLiveOrders)
		{
			Size = MessageSize;
			TypeCode = HftResponseTypeCode.Status;
			GridExTradeFee = gridExTradeFee;
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

		public readonly double GridExTradeFee;

		public readonly UserAssets UserAssets;

		public readonly UserLiveOrders UserLiveOrders;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<CurrentStatus>());
	}
}