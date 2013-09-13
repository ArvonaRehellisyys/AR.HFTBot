using System.Collections.Generic;
using System.Linq;

namespace AR.Hft.Process.Domain
{
    public class Portfolio : IPortfolio
    {
        public List<StockOwnership> Owned { get; set; }

        public Portfolio()
        {
            Owned = new List<StockOwnership>();
        }

        public void Add(string symbol, int amount)
        {
            var previous = FindOwnership(symbol);
            if (previous != null)
            {
                previous.Amount += amount;
                return;
            }

            Owned.Add(new StockOwnership
            {
                Symbol = symbol,
                Amount = amount
            });
        }

        public bool Has(string symbol, int amount)
        {
            var ownership = FindOwnership(symbol);
            return ownership != null && ownership.Amount >= amount;
        }

        private StockOwnership FindOwnership(string symbol)
        {
            return Owned.FirstOrDefault(x => x.Symbol == symbol);
        }
    }
}
