namespace AR.Hft.Process.Domain
{
    public interface IStockbroker
    {
        bool Buy(StockMessage ticker, int amount);
        bool Sell(StockMessage ticker, int amount);
        int GetPrice(StockMessage stockMessage);
    }

    public class StockBroker : IStockbroker
    {
        public bool Buy(StockMessage ticker, int amount)
        {
            throw new System.NotImplementedException();
        }

        public bool Sell(StockMessage ticker, int amount)
        {
            throw new System.NotImplementedException();
        }

        public int GetPrice(StockMessage stockMessage)
        {
            throw new System.NotImplementedException();
        }
    }
}
