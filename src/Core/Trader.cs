using System;
using System.Collections.Generic;

namespace Core
{
    public class Trader
    {
        public int Balance { get; set; }

        private readonly IStockbroker _stockbroker;
        private readonly IPortfolio _portfolio;

        private readonly List<StockSignal> _stockSignals = new List<StockSignal>();

        public Trader(IStockbroker stockbroker, IPortfolio portfolio)
        {
            _stockbroker = stockbroker;
            _portfolio = portfolio;
        }

        public void RegisterStock(TickerSymbol ticker, ISignal signal)
        {
            _stockSignals.Add(new StockSignal
            {
                Ticker = ticker,
                Signal = signal
            });
        }

        public void Trade()
        {
            foreach (var stockSignal in _stockSignals)
            {
                ProcessSignal(stockSignal);
            }
        }

        private void ProcessSignal(StockSignal stockSignal)
        {
            if (ShouldBuy(stockSignal) && HaveMoney(stockSignal))
            {
                _stockbroker.Buy(stockSignal.Ticker, 1);
            }
            else if (ShouldSell(stockSignal) && HaveStock(stockSignal))
            {
                _stockbroker.Sell(stockSignal.Ticker, 1);
                Balance += _stockbroker.GetPrice(stockSignal.Ticker);
            }
        }

        private bool HaveStock(StockSignal stockSignal)
        {
            return _portfolio.Has(stockSignal.Ticker, 1);
        }

        private static bool ShouldSell(StockSignal stockSignal)
        {
            return stockSignal.Assess(DateTime.Now) == -1;
        }

        private bool HaveMoney(StockSignal stockSignal)
        {
            return _stockbroker.GetPrice(stockSignal.Ticker) <= Balance;
        }

        private static bool ShouldBuy(StockSignal stockSignal)
        {
            return stockSignal.Assess(DateTime.Now) == 1;
        }
    }
}
