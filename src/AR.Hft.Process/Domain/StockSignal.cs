using System;

namespace AR.Hft.Process.Domain
{
    public class StockSignal
    {
        public TickerSymbol Ticker { get; set; }
        public ISignal Signal { get; set; }

        public int Assess(DateTime time)
        {
            return Signal.Assess(Ticker, time);
        }
    }
}