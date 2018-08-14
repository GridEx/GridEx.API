namespace GridEx.API.Trading.Requests
{
    public interface IHftOrder : IHftRequest
    {
		double Volume { get; }
    }
}