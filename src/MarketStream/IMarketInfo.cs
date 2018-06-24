﻿namespace GridEx.API.MarketStream
{
	public interface IMarketInfo
	{
		ushort Size { get; }

		MarketInfoTypeCode TypeCode { get; }

		int CopyTo(byte[] buffer, int offset = 0);
	}
}