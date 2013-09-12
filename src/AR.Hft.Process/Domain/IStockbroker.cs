namespace AR.Hft.Process.Domain
{
    public interface IStockbroker
    {
        bool Buy(TickerSymbol ticker, int amount);
        bool Sell(TickerSymbol ticker, int amount);
        int GetPrice(TickerSymbol tickerSymbol);
    }
}
