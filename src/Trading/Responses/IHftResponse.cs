namespace GridEx.API.Trading.Responses
{
	public interface IHftResponse
    {
		ushort Size { get; }

		ResponseTypeCode TypeCode { get; }
	}
}