namespace GridEx.API.Trading.Requests
{
	public interface IHftLimitOrder : IHftOrder
	{
		double Price { get; }
	}
}