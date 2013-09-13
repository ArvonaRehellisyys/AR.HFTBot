using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockValueReader
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] s = System.IO.File.ReadAllLines(@"E:\Code\AR.HFTBot\YahooApi.00.csv");

            for (int i = 0; i < s.Count() - 1; i++)
            {
                StockValue sv = new StockValue(s[i]);
                Console.WriteLine(string.Format("Bid = {0}, Ask = {1}, Symbol = {2}, Time = {3} {4}", sv.Bid, sv.Ask, sv.Symbol, sv.Time.ToShortDateString(), sv.Time.ToLongTimeString()));
            }

            Console.ReadLine();
        }
    }
}
