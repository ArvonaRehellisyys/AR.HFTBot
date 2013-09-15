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
        private List<StockMessage> _messages;
        private StockMessageBatch _batcher;

        [SetUp]
        public void Init()
        {
            _messages = new List<StockMessage>
            {
                new StockMessage {Symbol = "First", Time = new DateTime(2013, 1, 1)},
                new StockMessage {Symbol = "Second", Time = new DateTime(2013, 1, 1).AddMinutes(1)},
                new StockMessage {Symbol = "Third", Time = new DateTime(2013, 1, 1).AddMinutes(2)},
                new StockMessage {Symbol = "Fourth", Time = new DateTime(2013, 1, 1).AddMinutes(3)}

            };
            _batcher = new StockMessageBatch(_messages);
        }

        [Test]
        public void GetNextBatch_AtFirstTime_GetFirstPossibleMomentFromAvailableAndContinueFromThat()
        {
            const int minute = 60;

            var results = _batcher.GetNextBatch(minute);
            
            results.Count.Should().Be(1);
            results[0].Symbol.Should().Be("First");
        }

        [Test]
        public void GetNextBatch_WithBatchTimeContainingMultipleMessages_ReturnAll()
        {
            const int twoMinutes = 180;

            var results = _batcher.GetNextBatch(twoMinutes);

            results.Count.Should().Be(3);
            results[0].Symbol.Should().Be("First");
            results[1].Symbol.Should().Be("Second");
            results[2].Symbol.Should().Be("Third");
        }

        [Test]
        public void GetNextBatch_WithMultipleBatches_ContinueAfterAnother()
        {
            const int minute = 60;

            var results = _batcher.GetNextBatch(minute);
            results.Count.Should().Be(1);
            results[0].Symbol.Should().Be("First");

            results = _batcher.GetNextBatch(minute);
            results.Count.Should().Be(1);
            results[0].Symbol.Should().Be("Second");

            results = _batcher.GetNextBatch(minute);
            results.Count.Should().Be(1);
            results[0].Symbol.Should().Be("Third");
        }

        [Test]
        public void GetNextBatch_NoMoreData_ThrowException()
        {
            // Arrange.
            const int lotsOfMinutes = 600;
            _batcher.GetNextBatch(lotsOfMinutes);

            // Assert.
            _batcher.Invoking(x => x.GetNextBatch(60)).ShouldThrow<InvalidOperationException>("Fetched more items from batch but theres none left!");
        }

        [Test]
        public void HasMore_DataAvailable_ReturnTrue()
        {
            _batcher.GetNextBatch(60);

            _batcher.HasMore().Should().BeTrue();
        }

        [Test]
        public void HasMore_NoDataAvailable_ReturnFalse()
        {
            const int lotsOfMinutes = 600;
            _batcher.GetNextBatch(lotsOfMinutes);

            _batcher.HasMore().Should().BeFalse();
        }
    }
}
