namespace AR.Hft.Process.Domain
{
    public interface IStockbroker
    {
        bool Buy(string symbol, int amount);
        bool Sell(string symbol, int amount);
        int GetPrice(StockMessage stockMessage);
    }

    public class StockBroker : IStockbroker
    {
        public bool Buy(string symbol, int amount)
        {
            throw new System.NotImplementedException();
        }

        public bool Sell(string symbol, int amount)
        {
            throw new System.NotImplementedException();
        }

        public int GetPrice(StockMessage stockMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}
