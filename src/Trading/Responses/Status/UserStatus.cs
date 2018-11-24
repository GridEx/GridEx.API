using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses.Status
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public struct UserStatus : IHftResponse
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static unsafe ref UserStatus CopyFrom(byte[] array, int offset = 0)
		{
			fixed (byte* source = &array[offset])
			{
				return ref ((UserStatus*)source)[0];
			}
		}

		public ushort Size
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public HftResponseTypeCode TypeCode
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get;
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set;
		}

		public double GridExTradeFee;

		public UserAssets UserAssets;

		public UserLiveOrders UserLiveOrders;

		public static readonly ushort MessageSize = Convert.ToUInt16(Marshal.SizeOf<UserStatus>());
	}
}