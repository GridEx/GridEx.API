using System;

namespace GridEx.API.MarketStream
{
	public delegate void OnMarketSnapshotDelegate(MarketStreamSocket socket, ref MarketSnapshot snapshot);

	public sealed class MarketStreamSocket : GridExSocketBase
	{
		public Action<MarketStreamSocket, MarketChange> OnMarketChange = delegate { };

		public OnMarketSnapshotDelegate OnMarketSnapshot = delegate { };

		public Action<MarketStreamSocket, MarketStreamRestrictionsViolated> OnRestrictionsViolated = delegate { };

		public MarketStreamSocket()
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
					ref readonly MarketStreamRestrictionsViolated restrictionsViolated = ref MarketStreamRestrictionsViolated.CopyFrom(buffer, offset);
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