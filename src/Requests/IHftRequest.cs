using GridEx.API.Responses;

namespace GridEx.API.Requests
{
	public interface IHftRequest
    {
		ushort Size { get; }

		RequestTypeCode TypeCode { get; }

		long RequestId { get; }

		int CopyTo(byte[] buffer, int offset = 0);

		RejectReasonCode IsValid();
	}
}