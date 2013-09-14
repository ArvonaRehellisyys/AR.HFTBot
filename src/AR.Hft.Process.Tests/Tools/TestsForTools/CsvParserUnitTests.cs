using System;
using System.Collections.Generic;
using AR.Hft.Process.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace AR.Hft.Process.Tests.Tools.TestsForTools
{
    [TestFixture]
    public class CsvParserUnitTests
    {
        [Test]
        public void Parse_WithEmtpyContents_ReturnEmptyList()
        {
            CsvParser.Parse("").Count.Should().Be(0);
        }

        [Test]
        public void Parse_WithValidData_ReturnCorrectResults()
        {
            // Arrange.
            var expected = new List<StockMessage>
            {
                new StockMessage { Symbol = "PACQU", Ask = 15.29, Bid = 13.76, Time = new DateTime(2013, 9, 14, 17, 15, 14)},
                new StockMessage { Symbol = "ERB", Ask = 1.54, Bid = 1.51, Time = new DateTime(2013, 9, 14, 17, 15, 14)},
                new StockMessage { Symbol = "OCLR", Ask = 1.51, Bid = 1.52, Time = new DateTime(2013, 9, 14, 17, 15, 14)},
            };

            string source = "\"AskRealtime\",\"BidRealtime\",\"Symbol\",\"Time\"" + Environment.NewLine +
                            "\"15.29\",\"13.76\",\"PACQU\",\"2013-09-14 17:15:14 +03:00\"" + Environment.NewLine +
                            "\"1.54\",\"1.51\",\"ERB\",\"2013-09-14 17:15:14 +03:00\"" + Environment.NewLine +
                            "\"1.51\",\"1.52\",\"OCLR\",\"2013-09-14 17:15:14 +03:00\"";

            var results = CsvParser.Parse(source);

            expected.ShouldAllBeEquivalentTo(results);
        }
    }
}
