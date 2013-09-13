using System;
using System.Collections.Generic;

namespace AR.Hft.Process.Domain
{
    public class Trader
    {
        public int Balance { get; set; }

        private readonly IStockbroker _stockbroker;
        private readonly IPortfolio _portfolio;

        private readonly List<ISignal> _stockSignals = new List<ISignal>();

        public Trader(IStockbroker stockbroker, IPortfolio portfolio)
        {
            _stockbroker = stockbroker;
            _portfolio = portfolio;
        }

        public void RegisterStock(ISignal signal)
        {
            _stockSignals.Add(signal);
        }

        public void Trade()
        {
            foreach (var stockSignal in _stockSignals)
            {
                ProcessSignal(stockSignal);
            }
        }

        private void ProcessSignal(ISignal stockSignal)
        {
        }
    }
}
