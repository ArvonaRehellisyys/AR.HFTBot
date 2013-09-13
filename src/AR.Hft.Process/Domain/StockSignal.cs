using System;
using System.Collections.Generic;

namespace AR.Hft.Process.Domain
{
    public class StockSignal : ISignal
    {
        public StockSignal(List<StockMessage> stockHistory)
        {
            
        }

        public StockMessage Ticker { get; set; }
        public ISignal Signal { get; set; }

        public int Assess(string symbol)
        {
            throw new NotImplementedException();
        }
    }
}