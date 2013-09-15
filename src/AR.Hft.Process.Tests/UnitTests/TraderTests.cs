using AR.Hft.Process.Domain;
using AR.Hft.Process.Domain.Signals;
using FluentAssertions;
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
            _mockBroker.GetPrice("AAPL")
                       .Returns(100);
            _mockSignal.Assess()
                       .Returns(new Assessment { Recommendation = 1, Symbol = "AAPL" });

            // Act
            _trader.Register(_mockSignal);
            _trader.Trade();

            // Assert
            _mockBroker.DidNotReceive().Buy(Arg.Any<string>(), Arg.Any<int>());
            _mockPortfolio.DidNotReceive().Add(Arg.Any<string>(), Arg.Any<int>());
        }

        [Test]
        public void Trade_BuySignalReceived_BuysStock()
        {
            // Arrange
            _trader.Balance = 100;
            _mockBroker.GetPrice(Arg.Is<string>(x => x == "AAPL"))
                       .Returns(100);
            _mockSignal.Assess()
                        .Returns(new Assessment { Recommendation = 1, Symbol = "AAPL" });

            // Act
            _trader.Register(_mockSignal);
            _trader.Trade();

            // Assert
            _mockBroker.Received().Buy("AAPL", 1);
            _mockPortfolio.Received().Add("AAPL", 1);
        }

        [Test]
        public void Trade_SellSignalReceived_SellsStock()
        {
            // Arrange
            _mockSignal.Assess()
                       .Returns(new Assessment {Recommendation = -1, Symbol = "NOK"});
            _mockBroker.GetPrice("NOK")
                       .Returns(4);
            _mockPortfolio.Has("NOK", 1)
                       .Returns(true);
            _mockBroker.Sell("NOK", 1).Returns(4);

            // Act
            _trader.Register(_mockSignal);
            _trader.Trade();

            // Assert
            _mockBroker.Received().Sell("NOK", 1);
            _trader.Balance.Should().Be(4);
            _mockPortfolio.Received().Remove("NOK", 1);
        }

        [Test]
        public void Trade_NoStocksOwned_CannotSellStock()
        {
            // Arrange
            _mockSignal.Assess()
                       .Returns(new Assessment { Recommendation = -1, Symbol = "NOK" });

            // Act
            _trader.Register(_mockSignal);
            _trader.Trade();


            // Assert
            _mockBroker.DidNotReceive().Sell(Arg.Any<string>(), Arg.Any<int>());
            _mockPortfolio.DidNotReceive().Remove(Arg.Any<string>(), Arg.Any<int>());
        }
    }
}
