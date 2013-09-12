using System.Collections.Generic;

namespace AR.Hft.Process.Domain
{
    public interface IPortfolio
    {
        List<StockOwnership> Owned { get; set; }
        void Add(TickerSymbol tickerSymbol, int amount);
        bool Has(TickerSymbol tickerSymbol, int amount);
    }
}