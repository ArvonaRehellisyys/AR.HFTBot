using System.Collections.Generic;

namespace AR.Hft.Process.Domain
{
    public interface IPortfolio
    {
        List<StockOwnership> Owned { get; set; }
        void Add(StockMessage stockMessage, int amount);
        bool Has(StockMessage stockMessage, int amount);
    }
}