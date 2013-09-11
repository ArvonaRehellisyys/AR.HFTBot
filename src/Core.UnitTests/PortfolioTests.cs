using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace Core.UnitTests
{
    [TestFixture]
    public class PortfolioTests
    {
        private Portfolio _portfolio;

        [SetUp]
        public void SetUp()
        {
            _portfolio = new Portfolio();
        }

        [Test]
        public void Owned_NoneOwned_NoneReturned()
        {
            _portfolio.Owned.Should().BeEmpty();
        }

        [Test]
        public void Add_NoneOwnedPreviously_OneOwnedNow()
        {
            _portfolio.Add(new TickerSymbol { Name = "NOK" }, 1);

            _portfolio.Owned.ShouldBeEquivalentTo(new List<StockOwnership>
            {
                new StockOwnership
                {
                    Ticker = new TickerSymbol { Name = "NOK" },
                    Amount = 1
                }
            });
        }

        [Test]
        public void Add_OneOwnedPreviously_TwoOwnedNow()
        {
            _portfolio.Add(new TickerSymbol { Name = "NOK" }, 1);
            _portfolio.Add(new TickerSymbol { Name = "NOK" }, 1);

            _portfolio.Owned.ShouldBeEquivalentTo(new List<StockOwnership>
            {
                new StockOwnership
                {
                    Ticker = new TickerSymbol { Name = "NOK" },
                    Amount = 2
                }
            });
        }
    }
}
