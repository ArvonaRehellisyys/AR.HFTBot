using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Portfolio : IPortfolio
    {
        public List<StockOwnership> Owned { get; set; }

        public Portfolio()
        {
            Owned = new List<StockOwnership>();
        }

        public void Add(TickerSymbol tickerSymbol, int amount)
        {
            var previous = FindOwnership(tickerSymbol);
            if (previous != null)
            {
                previous.Amount += amount;
                return;
            }

            Owned.Add(new StockOwnership
            {
                Ticker = tickerSymbol,
                Amount = amount
            });
        }

        public bool Has(TickerSymbol tickerSymbol, int amount)
        {
            var ownership = FindOwnership(tickerSymbol);
            return ownership != null && ownership.Amount >= amount;
        }

        private StockOwnership FindOwnership(TickerSymbol tickerSymbol)
        {
            return Owned.FirstOrDefault(x => x.Ticker.Name == tickerSymbol.Name);
        }
    }
}
