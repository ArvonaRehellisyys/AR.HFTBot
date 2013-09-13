using System;
using AR.Hft.Process.Domain;
using FluentAssertions;
using FluentAssertions.Formatting;
using NSubstitute;
using NUnit.Framework;

namespace AR.Hft.Process.Tests.UnitTests
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
        public void Trade_NoMoney_CannotBuyGoodStock()
        {
            // Arrange
            _trader.Balance = 0;
            _mockBroker.GetPrice(Arg.Is<StockMessage>(x => x.Name == "AAPL"))
                       .Returns(100);
            _mockSignal.Assess("AAPL")
                       .Returns(1);

            // Act
            _trader.RegisterStock(_mockSignal);
            _trader.Trade();

            // Assert
            _mockBroker.DidNotReceive().Buy(Arg.Any<StockMessage>(), Arg.Any<int>());
        }

        [Test]
        public void Trade_BuySignalReceived_BuysStock()
        {
            // Arrange
            _trader.Balance = 100;
            _mockBroker.GetPrice(Arg.Is<StockMessage>(x => x.Name == "AAPL"))
                       .Returns(100);
            _mockSignal.Assess("AAPL")
                       .Returns(1);

            // Act
            _trader.RegisterStock(_mockSignal);
            _trader.Trade();

            // Assert
            _mockBroker.Received().Buy(Arg.Is<StockMessage>(x => x.Name == "AAPL"),
                                      Arg.Is(1));
        }

        [Test]
        public void Trade_SellSignalReceived_SellsStock()
        {
            // Arrange
            _mockSignal.Assess("NOK")
                       .Returns(-1);
            _mockBroker.GetPrice(Arg.Is<StockMessage>(x => x.Name == "NOK"))
                       .Returns(4);
            _mockPortfolio.Has(Arg.Is<StockMessage>(x => x.Name == "NOK"), 1)
                          .Returns(true);

            // Act
            _trader.RegisterStock(_mockSignal);
            _trader.Trade();


            // Assert
            _mockBroker.Received().Sell(Arg.Is<StockMessage>(x => x.Name == "NOK"),
                                       Arg.Is(1));
            _trader.Balance.Should().Be(4);
        }

        [Test]
        public void Trade_NoStocksOwned_CannotSellStock()
        {
            // Arrange
            _mockSignal.Assess("NOK")
                       .Returns(-1);

            // Act
            _trader.RegisterStock(_mockSignal);
            _trader.Trade();


            // Assert
            _mockBroker.DidNotReceive().Sell(Arg.Any<StockMessage>(), Arg.Any<int>());
        }
    }
}
