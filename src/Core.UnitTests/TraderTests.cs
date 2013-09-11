using System;
using NSubstitute;
using NUnit.Framework;

namespace Core.UnitTests
{
    [TestFixture]
    public class TraderTests
    {
        [Test]
        public void Trade_BuySignalReceived_BuysStock()
        {
            // Arrange
            var mockBroker = Substitute.For<IStockbroker>();

            var mockSignal = Substitute.For<ISignal>();
            mockSignal.Assess(Arg.Any<TickerSymbol>(), Arg.Any<DateTime>())
                      .Returns(1);


            // Act
            var trader = new Trader(mockBroker);
            trader.RegisterStock(new TickerSymbol { Name = "AAPL" }, mockSignal);
            trader.Trade();


            // Assert
            mockBroker.Received().Buy(Arg.Is<TickerSymbol>(x => x.Name == "AAPL"),
                                      Arg.Is(1));
        }

        [Test]
        public void Trade_SellSignalReceived_SellsStock()
        {
            // Arrange
            var mockBroker = Substitute.For<IStockbroker>();

            var mockSignal = Substitute.For<ISignal>();
            mockSignal.Assess(Arg.Any<TickerSymbol>(), Arg.Any<DateTime>())
                      .Returns(-1);


            // Act
            var trader = new Trader(mockBroker);
            trader.RegisterStock(new TickerSymbol { Name = "NOK" }, mockSignal);
            trader.Trade();


            // Assert
            mockBroker.Received().Sell(Arg.Is<TickerSymbol>(x => x.Name == "NOK"),
                                       Arg.Is(1));
        }
    }
}
