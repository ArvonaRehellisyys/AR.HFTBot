using System.Collections.Generic;

namespace AR.Hft.Process.Domain
{
    public interface IPortfolio
    {
        List<StockOwnership> Owned { get; set; }
        void Add(string symbol, int amount);
        bool Has(string symbol, int amount);
    }
}