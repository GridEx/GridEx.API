﻿using System;
using GridEx.API.MarketStream;

namespace GridEx.API
{
	public sealed class MarketStreamSocket : GridExSocketBase
	{
		public Action<MarketStreamSocket, MarketChange> OnMarketChange = delegate { };

		public Action<MarketStreamSocket, MarketSnapshot> OnMarketSnapshot = delegate { };
		
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
					ref readonly MarketSnapshot marketSnapshot = ref MarketSnapshot.CopyFrom(buffer, offset);
					OnMarketSnapshot(this, marketSnapshot);
					break;
				default:
					;
					// unknown response
					break;
			}
		}
	}
}