namespace GridEx.API.Trading.Responses
{
	public interface IHftResponse
    {
		ushort Size { get; }

		HftResponseTypeCode TypeCode { get; }
	}
}