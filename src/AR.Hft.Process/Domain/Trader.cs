using System.Collections.Generic;
using AR.Hft.Process.Domain.Signals;

namespace AR.Hft.Process.Domain
{
    public class Trader
    {
        public double Balance { get; set; }

        private readonly IStockbroker _stockbroker;
        private readonly IPortfolio _portfolio;

        private readonly List<ISignal> _stockSignals = new List<ISignal>();

        public Trader(IStockbroker stockbroker, IPortfolio portfolio)
        {
            _stockbroker = stockbroker;
            _portfolio = portfolio;
        }

        public void Register(ISignal signal)
        {
            _stockSignals.Add(signal);
        }

        public void Trade()
        {
            foreach (var stockSignal in _stockSignals)
            {
                var assesment = stockSignal.Assess();
                
                if (assesment.Recommendation > 0)
                {
                    Balance -= _stockbroker.Buy(assesment.Symbol, 1);
                }
                else if (assesment.Recommendation < 0)
                {
                    Balance += _stockbroker.Sell(assesment.Symbol, 1);
                }
            }
        }
    }
}
