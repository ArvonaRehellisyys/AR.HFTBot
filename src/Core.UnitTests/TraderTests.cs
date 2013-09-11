using System;
using NSubstitute;
using NUnit.Framework;

namespace Core.UnitTests
{
    [TestFixture]
    public class TraderTests
    {
        private IStockbroker _mockBroker;
        private ISignal _mockSignal;
        private IPortfolio _mockPortfolio;

        private Trader _trader;

        [SetUp]
        public void SetUp()
        {
            _mockBroker = Substitute.For<IStockbroker>();
            _mockSignal = Substitute.For<ISignal>();
            _mockPortfolio = Substitute.For<IPortfolio>();

            _trader = new Trader(_mockBroker, _mockPortfolio);
        }

        [Test]
        public void Trade_BuySignalReceived_BuysStock()
        {
            // Arrange
            _mockSignal.Assess(Arg.Any<TickerSymbol>(), Arg.Any<DateTime>())
                      .Returns(1);

            // Act
            _trader.RegisterStock(new TickerSymbol { Name = "AAPL" }, _mockSignal);
            _trader.Trade();

            // Assert
            _mockBroker.Received().Buy(Arg.Is<TickerSymbol>(x => x.Name == "AAPL"),
                                      Arg.Is(1));
        }

        [Test]
        public void Trade_SellSignalReceived_SellsStock()
        {
            // Arrange
            _mockSignal.Assess(Arg.Any<TickerSymbol>(), Arg.Any<DateTime>())
                      .Returns(-1);
            _mockPortfolio.Has(Arg.Is<TickerSymbol>(x => x.Name == "NOK"), 1)
                          .Returns(true);

            // Act
            _trader.RegisterStock(new TickerSymbol { Name = "NOK" }, _mockSignal);
            _trader.Trade();


            // Assert
            _mockBroker.Received().Sell(Arg.Is<TickerSymbol>(x => x.Name == "NOK"),
                                       Arg.Is(1));
        }

        [Test]
        public void Trade_NoStocksOwned_CannotSellStock()
        {
            // Arrange
            _mockSignal.Assess(Arg.Any<TickerSymbol>(), Arg.Any<DateTime>())
                      .Returns(-1);

            // Act
            _trader.RegisterStock(new TickerSymbol { Name = "NOK" }, _mockSignal);
            _trader.Trade();


            // Assert
            _mockBroker.DidNotReceive().Sell(Arg.Any<TickerSymbol>(), Arg.Any<int>());
        }
    }
}
