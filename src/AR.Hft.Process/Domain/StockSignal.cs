using System;
using System.Collections.Generic;

namespace AR.Hft.Process.Domain
{
    public class StockSignal : ISignal
    {
        private readonly List<StockMessage> _stockHistory;

        public StockSignal(List<StockMessage> stockHistory, string stock)
        {
            _stockHistory = stockHistory;
        }

        public Assessment Assess()
        {
            return new Assessment();
        }
    }
}