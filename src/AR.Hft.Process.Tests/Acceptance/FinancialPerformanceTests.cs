using System;
using System.Collections.Generic;
using AR.Hft.Process.Domain;
using NSubstitute;
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
        public void Foo()
        {
            var now = new DateTime(2013, 9, 10);

            var history = new List<StockMessage>
            {
                new StockMessage {Symbol = "NOK", Ask = 5.9, Bid = 5.7, Time = now.AddMinutes(-1)}
            };

            var nokSignal = new StockSignal(history, "NOK");
            var jormaSignal = new StockSignal(history, "JORMA");

            var portfolio = new Portfolio();
            var stockBroker = Substitute.For<IStockbroker>();
            var trader = new Trader(stockBroker, portfolio);

            trader.Register(nokSignal);
            trader.Register(jormaSignal);

            var message = new StockMessage
            {
                Symbol = "NOK",
                Bid = 6.54,
                Ask = 6.70,
                Time = now
            };

            history.Add(message);

            trader.Trade();

            stockBroker.Received().Buy("NOK", 1);
        }
    }
}
