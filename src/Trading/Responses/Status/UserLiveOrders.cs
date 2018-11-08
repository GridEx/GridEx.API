using System.Runtime.InteropServices;

namespace GridEx.API.Trading.Responses.Status
{
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	public unsafe struct UserLiveOrders
	{
		public UserLiveOrders(byte quantity)
		{
			Quantity = quantity;
		}

		public byte Quantity;
		public fixed long Ids[MaxLiveOrdersQuantity];
		public fixed long Times[MaxLiveOrdersQuantity];
		public fixed byte Types[MaxLiveOrdersQuantity]; // HftOrderTypeCode
		public fixed double Prices[MaxLiveOrdersQuantity];
		public fixed double UnfilledVolumes[MaxLiveOrdersQuantity];

		public const int MaxLiveOrdersQuantity = 25;
	}
}