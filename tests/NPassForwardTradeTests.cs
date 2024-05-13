using Kimmen.SilverTrade;

namespace silver_trade.test
{
    public class NPassForwardTradeTests
    {
        [Fact]
        public void Random10Prices()
        {
            List<int> prices = [100, 180, 260, 310, 40, 535, 695, 200, 400, 600];
            var trader = new NPassForwardTrade(new APICaller(prices));

            Assert.True(trader.GetBuyDay() == 4);
            Assert.True(trader.GetSellDay() == 6);
        }

        [Fact]
        public void Random20Prices()
        {
            List<int> prices = [200, 150, 300, 250, 400, 350, 500, 450, 600, 550, 700, 650, 800, 750, 900, 850, 1000, 950, 1100, 1050];
            var trader = new NPassForwardTrade(new APICaller(prices));

            Assert.True(trader.GetBuyDay() == 1);
            Assert.True(trader.GetSellDay() == 18);
        }

        [Fact]
        public void AlwaysAscending()
        {
            List<int> prices = [953, 964, 972, 979, 985, 990, 994, 997, 999, 1000];
            var trader = new NPassForwardTrade(new APICaller(prices));

            Assert.True(trader.GetBuyDay() == 0);
            Assert.True(trader.GetSellDay() == 9);
        }

        [Fact]
        public void AlwaysDescending()
        {
            List<int> prices = [1000, 999, 997, 994, 990, 985, 979, 972, 964, 953];
            var trader = new NPassForwardTrade(new APICaller(prices));

            Assert.True(trader.GetBuyDay() == 0);
            Assert.True(trader.GetSellDay() == 1);
        }

        [Fact]
        public void ConstantPrice()
        {
            List<int> prices = [500, 500, 500, 500, 500, 500, 500, 500, 500, 500];
            var trader = new NPassForwardTrade(new APICaller(prices));

            Assert.True(trader.GetBuyDay() == 0);
            Assert.True(trader.GetSellDay() == 1);
        }

        [Fact]
        public void VShapedPriceCurve()
        {
            List<int> prices = [800, 700, 600, 500, 400, 300, 400, 500, 600, 700, 800];
            var trader = new TwoPassBackForwardTrade(new APICaller(prices));

            Assert.True(trader.GetBuyDay() == 5);
            Assert.True(trader.GetSellDay() == 10);
        }

        [Fact]
        public void AShapedPriceCurve()
        {
            List<int> prices = [200, 300, 400, 500, 600, 700, 600, 500, 400, 300, 200];
            var trader = new TwoPassBackForwardTrade(new APICaller(prices));

            Assert.True(trader.GetBuyDay() == 0);
            Assert.True(trader.GetSellDay() == 5);
        }
    }
}