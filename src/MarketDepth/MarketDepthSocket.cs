using System;
using GridEx.API.MarketDepth.Responses;

namespace GridEx.API.MarketDepth
{
	public delegate void OnMarketSnapshotLevel1Delegate(MarketDepthSocket socket, ref MarketSnapshotLevel1 snapshot);

	public delegate void OnMarketSnapshotLevel2Delegate(MarketDepthSocket socket, ref MarketSnapshotLevel2 snapshot);

	public delegate void OnMarketSnapshotLevel3Delegate(MarketDepthSocket socket, ref MarketSnapshotLevel3 snapshot);

	public sealed class MarketDepthSocket : GridExSocketBase
	{
		public Action<MarketDepthSocket, MarketChange> OnMarketChange = delegate { };

		public OnMarketSnapshotLevel1Delegate OnMarketSnapshotLevel1 = delegate { };

		public OnMarketSnapshotLevel2Delegate OnMarketSnapshotLevel2 = delegate { };

		public OnMarketSnapshotLevel3Delegate OnMarketSnapshotLevel3 = delegate { };

		public Action<MarketDepthSocket, MarketDepthRestrictionsViolated> OnRestrictionsViolated = delegate { };

		public MarketDepthSocket()
			: base(MarketInfoSize.Max)
		{
		}

		protected override void CreateResponse(byte[] buffer, int offset)
		{
			switch ((MarketInfoTypeCode)buffer[offset + 2])
			{
				case MarketInfoTypeCode.MarketChange:
					ref readonly MarketChange marketChange = ref MarketChange.CopyFrom(buffer, offset);
					OnMarketChange(this, marketChange);
					break;
				case MarketInfoTypeCode.MarketSnapshotLevel1:
					ref MarketSnapshotLevel1 marketSnapshotLevel1 = ref MarketSnapshotLevel1.CopyFrom(buffer, offset);
					OnMarketSnapshotLevel1(this, ref marketSnapshotLevel1);
					break;
				case MarketInfoTypeCode.MarketSnapshotLevel2:
					ref MarketSnapshotLevel2 marketSnapshotLevel2 = ref MarketSnapshotLevel2.CopyFrom(buffer, offset);
					OnMarketSnapshotLevel2(this, ref marketSnapshotLevel2);
					break;
				case MarketInfoTypeCode.MarketSnapshotLevel3:
					ref MarketSnapshotLevel3 marketSnapshotLevel3 = ref MarketSnapshotLevel3.CopyFrom(buffer, offset);
					OnMarketSnapshotLevel3(this, ref marketSnapshotLevel3);
					break;
				case MarketInfoTypeCode.RestrictionsViolated:
					ref readonly MarketDepthRestrictionsViolated restrictionsViolated = ref MarketDepthRestrictionsViolated.CopyFrom(buffer, offset);
					OnRestrictionsViolated(this, restrictionsViolated);
					break;
				default:
					;
					// unknown response
					break;
			}
		}
	}
}