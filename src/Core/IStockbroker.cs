namespace Core
{
    public interface IStockbroker
    {
        bool Buy(TickerSymbol ticker, int amount);
        bool Sell(TickerSymbol ticker, int amount);
        int GetPrice(TickerSymbol tickerSymbol);
    }
}
