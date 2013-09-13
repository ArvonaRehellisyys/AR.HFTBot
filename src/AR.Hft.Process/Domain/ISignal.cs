using System;

namespace AR.Hft.Process.Domain
{
    public interface ISignal
    {
        int Assess(string symbol);
    }
}