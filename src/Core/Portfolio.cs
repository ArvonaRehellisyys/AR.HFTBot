using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Portfolio
    {
        public List<StockOwnership> Owned { get; set; }

        public Portfolio()
        {
            Owned = new List<StockOwnership>();
        }

        public void Add(TickerSymbol tickerSymbol, int amount)
        {
            var previous = Owned.FirstOrDefault(x => x.Ticker.Name == tickerSymbol.Name);
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
    }
}
