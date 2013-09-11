using System.Collections.Generic;

namespace Core
{
    public interface IPortfolio
    {
        List<StockOwnership> Owned { get; set; }
        void Add(TickerSymbol tickerSymbol, int amount);
        bool Has(TickerSymbol tickerSymbol, int amount);
    }
}