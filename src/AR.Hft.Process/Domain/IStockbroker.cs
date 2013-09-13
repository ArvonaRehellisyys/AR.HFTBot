namespace AR.Hft.Process.Domain
{
    public interface IStockbroker
    {
        double Buy(string symbol, int amount);
        double Sell(string symbol, int amount);
        int GetPrice(string stockMessage);
    }
}
