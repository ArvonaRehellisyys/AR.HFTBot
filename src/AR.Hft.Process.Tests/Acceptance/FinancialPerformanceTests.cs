using System;
using System.Collections.Generic;
using AR.Hft.Process.Domain;
using AR.Hft.Process.Domain.Signals;
using AR.Hft.Process.Tests.Tools;
using NUnit.Framework;

namespace AR.Hft.Process.Tests.Acceptance
{
    [TestFixture]
    public class FinancialPerformanceTests
    {
        [SetUp]
        public void Init()
        {
            
        }

        [Test]
        public void RandomSignal()
        {
            var now = new DateTime(2013, 9, 10);

            var history = new List<StockMessage>
            {
                new StockMessage { Symbol = "PACQU", Ask = 24.0, Bid = 13.50, Time = now.AddMinutes(-5) },
                new StockMessage { Symbol = "PACQU", Ask = 27.84, Bid = 13.50, Time = now.AddMinutes(-4) },
                new StockMessage { Symbol = "PACQU", Ask = 22.50, Bid = 13.50, Time = now.AddMinutes(-3) },
                new StockMessage { Symbol = "PACQU", Ask = 17.85, Bid = 13.50, Time = now.AddMinutes(-2) },
                new StockMessage { Symbol = "PACQU", Ask = 13.79, Bid = 11.00, Time = now.AddMinutes(-1) }
            };

            var random = new RandomSignal("PACQU");

            var portfolio = new Portfolio();
            var stockBroker = new StubStockbroker(history);
            var trader = new Trader(stockBroker, portfolio);

            trader.Register(random);

            stockBroker.CurrentTime = now.AddMinutes(-5);
            for (int i = 0; i < 5; i++)
            {
                stockBroker.CurrentTime = stockBroker.CurrentTime.AddMinutes(1);
                trader.Trade();
            }

            //trader.Balance.Should().BeGreaterThan(0);
        }

        [Test]
        public void RandomSignalBatchVersion()
        {
            var now = new DateTime(2013, 9, 10);

            var history = new List<StockMessage>
            {
                new StockMessage { Symbol = "PACQU", Ask = 24.0, Bid = 13.50, Time = now.AddMinutes(-7) },
                new StockMessage { Symbol = "PACQU", Ask = 27.84, Bid = 13.50, Time = now.AddMinutes(-6) },
            };

            var upcomingData = new List<StockMessage>
            {
                new StockMessage { Symbol = "PACQU", Ask = 24.0, Bid = 13.50, Time = now.AddMinutes(-5) },
                new StockMessage { Symbol = "PACQU", Ask = 27.84, Bid = 13.50, Time = now.AddMinutes(-4) },
                new StockMessage { Symbol = "PACQU", Ask = 22.50, Bid = 13.50, Time = now.AddMinutes(-3) },
                new StockMessage { Symbol = "PACQU", Ask = 17.85, Bid = 13.50, Time = now.AddMinutes(-2) },
                new StockMessage { Symbol = "PACQU", Ask = 13.79, Bid = 11.00, Time = now.AddMinutes(-1) }
            };

            var random = new RandomSignal("PACQU");

            var portfolio = new Portfolio();
            var stockBroker = new StubStockbroker2(history);
            var trader = new Trader(stockBroker, portfolio);

            trader.Register(random);

            var batcher = new StockMessageBatch(upcomingData);

            const int minute = 60;
            while (batcher.HasMore())
            {
                batcher.GetNextBatch(minute).ForEach(history.Add);
                trader.Trade();                
            }
        }
    }
}
