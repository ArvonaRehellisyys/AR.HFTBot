namespace AR.Hft.Process.Domain
{
    public interface IStockbroker : IStockExchange
    {
        double Buy(string symbol, int amount);
        double Sell(string symbol, int amount);
    }
}
