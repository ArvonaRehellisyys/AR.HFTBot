using System;
using System.Collections.Generic;
using System.Linq;
using AR.Hft.Process.Domain;

namespace AR.Hft.Process.Tests.Acceptance
{
    public class StubStockbroker2 : IStockbroker
    {
        private readonly List<StockMessage> _stockMessages;

        public DateTime CurrentTime { get; set; }

        public StubStockbroker2(List<StockMessage> stockMessages)
        {
            _stockMessages = stockMessages;
        }

        public double GetPrice(string stockMessage)
        {
            var msg = _stockMessages
                .OrderByDescending(x => x.Time)
                .FirstOrDefault();
            return msg.Ask;
        }

        public double Buy(string symbol, int amount)
        {
            var msg = _stockMessages.Where(x => x.Symbol == symbol)
                .OrderByDescending(x => x.Time)
                .FirstOrDefault();
            return msg.Ask;
        }

        public double Sell(string symbol, int amount)
        {
            var msg = _stockMessages.Where(x => x.Symbol == symbol)
                .OrderByDescending(x => x.Time)
                .FirstOrDefault();
            return msg.Ask;
        }
    }
}