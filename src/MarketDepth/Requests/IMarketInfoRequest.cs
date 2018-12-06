using GridEx.API.MarketDepth.Responses;

namespace GridEx.API.MarketDepth.Requests
{
	public interface IMarketInfoRequest : ISizeable
	{
		MarketInfoRequestTypeCode TypeCode { get; }

		long RequestId { get; }

		int CopyTo(byte[] buffer, int offset = 0);

		MarketInfoRejectReasonCode IsValid();
	}
}
