namespace GridEx.API.Responses
{
	public interface IHftResponse
    {
		ushort Size { get; }

		ResponseTypeCode TypeCode { get; }

		int CopyTo(byte[] buffer, int offset = 0);
	}
}