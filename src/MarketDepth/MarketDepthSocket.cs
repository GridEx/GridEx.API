using System;
using GridEx.API.MarketDepth.Responses;

namespace GridEx.API.MarketDepth
{
	public delegate void OnMarketSnapshotDelegate(MarketDepthSocket socket, ref MarketSnapshot snapshot);

	public sealed class MarketDepthSocket : GridExSocketBase
	{
		public Action<MarketDepthSocket, MarketChange> OnMarketChange = delegate { };

		public OnMarketSnapshotDelegate OnMarketSnapshot = delegate { };

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
				case MarketInfoTypeCode.MarketSnapshot:
					ref MarketSnapshot marketSnapshot = ref MarketSnapshot.CopyFrom(buffer, offset);
					OnMarketSnapshot(this, ref marketSnapshot);
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