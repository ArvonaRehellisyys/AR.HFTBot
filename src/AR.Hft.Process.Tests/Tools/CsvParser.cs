using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using AR.Hft.Process.Domain;

namespace AR.Hft.Process.Tests.Tools
{
    public static class CsvParser
    {
        public static List<StockMessage> Parse(string csvFileContents)
        {
            var lines = RemoveHeadersAndSplitLines(csvFileContents);

            return lines.Select(line =>line.Split(',').ToList())
                .Select(expanded => new StockMessage
                {
                    Symbol = expanded[2], 
                    Ask = ParseDouble(expanded[0]), 
                    Bid = ParseDouble(expanded[1]),
                    Time = ParseDateTime(expanded[3])
                })
                .ToList();
        }

        private static IEnumerable<string> RemoveHeadersAndSplitLines(string contents)
        {
            var lines = contents.Split(new[] { '\n' }).ToList();
            lines.RemoveAt(0);

            var clean = new List<string>();
            lines.ForEach(line => clean.Add(
                line.Replace("\r","").Replace("\"","")));

            return clean;
        }

        private static double ParseDouble(string input)
        {
            // Double requires , not . character for parsing.
            input = input.Replace(".", ",");

            return Double.Parse(input);
        }

        private static DateTime ParseDateTime(string input)
        {
            return DateTime.ParseExact(input, "yyyy-MM-dd HH:mm:ss zzz", CultureInfo.InvariantCulture);
        }
    }
}
