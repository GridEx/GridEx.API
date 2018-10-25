using System;
using GridEx.API.MarketDepth.Responses;

namespace GridEx.API.MarketDepth
{
	public delegate void OnMarketSnapshotLevel1Delegate(MarketDepthSocket socket, ref MarketSnapshotLevel1 snapshot);

	public delegate void OnMarketSnapshotLevel2Delegate(MarketDepthSocket socket, ref MarketSnapshotLevel2 snapshot);

	public delegate void OnMarketSnapshotLevel3Delegate(MarketDepthSocket socket, ref MarketSnapshotLevel3 snapshot);

	public sealed class MarketDepthSocket : GridExSocketBase
	{
		public event Action<MarketDepthSocket, MarketChange> OnMarketChange;

		public event OnMarketSnapshotLevel1Delegate OnMarketSnapshotLevel1;

		public event OnMarketSnapshotLevel2Delegate OnMarketSnapshotLevel2;

		public event OnMarketSnapshotLevel3Delegate OnMarketSnapshotLevel3;

		public event Action<MarketDepthSocket, MarketDepthRestrictionsViolated> OnRestrictionsViolated;

		public event Action<MarketDepthSocket, MarketDepthSettings> OnSettings;

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
					OnMarketChange?.Invoke(this, marketChange);
					break;
				case MarketInfoTypeCode.MarketSnapshotLevel1:
					ref MarketSnapshotLevel1 marketSnapshotLevel1 = ref MarketSnapshotLevel1.CopyFrom(buffer, offset);
					OnMarketSnapshotLevel1?.Invoke(this, ref marketSnapshotLevel1);
					break;
				case MarketInfoTypeCode.MarketSnapshotLevel2:
					ref MarketSnapshotLevel2 marketSnapshotLevel2 = ref MarketSnapshotLevel2.CopyFrom(buffer, offset);
					OnMarketSnapshotLevel2?.Invoke(this, ref marketSnapshotLevel2);
					break;
				case MarketInfoTypeCode.MarketSnapshotLevel3:
					ref MarketSnapshotLevel3 marketSnapshotLevel3 = ref MarketSnapshotLevel3.CopyFrom(buffer, offset);
					OnMarketSnapshotLevel3?.Invoke(this, ref marketSnapshotLevel3);
					break;
				case MarketInfoTypeCode.RestrictionsViolated:
					ref readonly MarketDepthRestrictionsViolated restrictionsViolated = ref MarketDepthRestrictionsViolated.CopyFrom(buffer, offset);
					OnRestrictionsViolated?.Invoke(this, restrictionsViolated);
					break;
				case MarketInfoTypeCode.MarketDepthSettings:
					ref MarketDepthSettings settings = ref MarketDepthSettings.CopyFrom(buffer, offset);
					OnSettings?.Invoke(this, settings);
					break;
				default:
					;
					// unknown response
					break;
			}
		}
	}
}