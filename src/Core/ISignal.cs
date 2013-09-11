using System;

namespace Core
{
    public interface ISignal
    {
        int Assess(TickerSymbol ticker, DateTime time);
    }
}
