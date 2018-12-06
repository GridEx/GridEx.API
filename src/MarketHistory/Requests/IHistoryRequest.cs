using GridEx.API.MarketHistory.Responses;

namespace GridEx.API.MarketHistory.Requests
{
	public interface IHistoryRequest
	{
		ushort Size { get; }

		HistoryRequestTypeCode TypeCode { get; }

		long RequestId { get; }

		int CopyTo(byte[] buffer, int offset = 0);

		HistoryRejectReasonCode IsValid();
	}
}
