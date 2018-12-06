namespace GridEx.API.Trading.Responses
{
	public interface IHftResponse : ISizeable
    {
		HftResponseTypeCode TypeCode { get; }
	}
}