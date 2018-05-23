namespace GridEx.API.Responses
{
	public interface IHftResponse
    {
		byte Size { get; }

		ResponseTypeCode TypeCode { get; }

		int CopyTo(byte[] buffer, int offset = 0);
	}
}