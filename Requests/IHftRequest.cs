using GridEx.API.Responses;

namespace GridEx.API.Requests
{
	public interface IHftRequest
    {
		byte Size { get; }

		RequestTypeCode TypeCode { get; }

		int RequestId { get; }

		int CopyTo(byte[] buffer, int offset = 0);

		RejectReasonCode IsValid();
	}
}