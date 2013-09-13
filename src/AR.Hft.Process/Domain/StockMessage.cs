using System;

namespace AR.Hft.Process.Domain
{
    public class StockMessage
    {
        public string Name { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
        public DateTime Time { get; set; }
    }
}