using System;
using System.Linq;

namespace StockValueReader
{
    class StockValue
    {
        private double _ask;
        private double _bid;
        private string _symbol = "";
        private DateTime _time;

        public StockValue(string csvLine)
        {
            csvLine = csvLine.Substring(1, csvLine.Length - 2).Replace("\",\"", ",");
            string[] foo = csvLine.Split(',');

            double ask;
            double bid;
            string symbol;
            DateTime time;
            StringTools st = new StringTools();

            if (foo.Count() == 4 && double.TryParse(foo[0].Replace(".", ","), out ask) && double.TryParse(foo[1].Replace(".", ","), out bid))
            {
                _bid = bid;
                _ask = ask;
                _symbol = foo[2];
                _time = st.ParseDateTimeFromString(foo[3]);
            }
        }

        public double Bid { get { return _bid; } }
        public double Ask { get { return _ask; } }
        public string Symbol { get { return _symbol; } }
        public DateTime Time { get { return _time; } }
    }
}
