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

        public void Add(StockMessage stockMessage, int amount)
        {
            var previous = FindOwnership(stockMessage);
            if (previous != null)
            {
                previous.Amount += amount;
                return;
            }

            Owned.Add(new StockOwnership
            {
                Ticker = stockMessage,
                Amount = amount
            });
        }

        public bool Has(StockMessage stockMessage, int amount)
        {
            var ownership = FindOwnership(stockMessage);
            return ownership != null && ownership.Amount >= amount;
        }

        private StockOwnership FindOwnership(StockMessage stockMessage)
        {
            return Owned.FirstOrDefault(x => x.Ticker.Name == stockMessage.Name);
        }
    }
}
