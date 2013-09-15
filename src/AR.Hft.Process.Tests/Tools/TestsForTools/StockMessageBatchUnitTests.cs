using System;
using System.Collections.Generic;
using AR.Hft.Process.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace AR.Hft.Process.Tests.Tools.TestsForTools
{
    [TestFixture]
    public class StockMessageBatchUnitTests
    {
        [SetUp]
        public void Init()
        {
            
        }

        [Test]
        public void GetNextBatch_AtFirstTime_GetFirstPossibleMomentFromAvailableAndContinueFromThat()
        {
            const int minute = 60;

            var messages = new List<StockMessage>
            {
                new StockMessage {Symbol = "ThisShouldExist", Time = new DateTime(2013, 1, 1)},
                new StockMessage {Symbol = "ThisShouldNotExist", Time = new DateTime(2013, 1, 1).AddMinutes(2)}
            };

            var batcher = new StockMessageBatch(messages);
            var results = batcher.GetNextBatch(minute);
            results.Count.Should().Be(1);
            results[0].Symbol.Should().Be("ThisShouldExist");
        }
    }
}
