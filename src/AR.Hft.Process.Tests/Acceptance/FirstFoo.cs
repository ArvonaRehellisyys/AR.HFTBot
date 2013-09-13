using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using AR.Hft.Process.Domain;
using NSubstitute;
using NUnit.Framework;

namespace AR.Hft.Process.Tests.Acceptance
{
    [TestFixture]
    public class FirstFoo
    {
        [SetUp]
        public void Init()
        {
            
        }

        private class Handler
        {
            private readonly Trader _trader;

            public Handler(Trader trader)
            {
                _trader = trader;
            }

            public void Run(StockMessage message)
            {
                   
            }
        }

        [Test]
        public void Foo()
        {
            var now = new DateTime(2013, 9, 10);

            var history = new List<StockMessage>
            {
                new StockMessage() {Name = "NOK", Ask = 5.9, Bid = 5.7, Time = now.AddMinutes(-1)}
            };

            var nokSignal = new StockSignal(history);

            var portfolio = new Portfolio();
            var stockBroker = new StockBroker();
            var trader = new Trader(stockBroker, portfolio);

            var message = new StockMessage
            {
                Name = "NOK",
                Bid = 6.54,
                Ask = 6.70,
                Time = now
            };

            var boo = new Handler(trader);
            boo.Run(message);

        }
    }
}
