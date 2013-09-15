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
                var assessment = stockSignal.Assess();
                
                if (assessment.Recommendation > 0)
                {
                    const int amount = 1;
                    var price = _stockbroker.GetPrice(assessment.Symbol) * amount;
                    if (Balance >= price)
                    {
                        Balance -= _stockbroker.Buy(assessment.Symbol, amount);
                        _portfolio.Add(assessment.Symbol, amount);
                    }
                }
                else if (assessment.Recommendation < 0)
                {
                    const int amount = 1;

                    if (_portfolio.Has(assessment.Symbol, amount))
                    {
                        Balance += _stockbroker.Sell(assessment.Symbol, amount);
                        _portfolio.Remove(assessment.Symbol, amount);
                    }
                }
            }
        }
    }
}
