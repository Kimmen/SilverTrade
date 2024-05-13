namespace Kimmen.SilverTrade;

public class TwoPassBackForwardTrade : ISilverTrade
{
    private int _buyDay;
    private int _sellDay; 

    public TwoPassBackForwardTrade(APICaller api)
    {
        var (prices, highest) = this.PreparePrices(api);
        var mostProfit = int.MinValue;
        for (var i = 0; i < prices.Count - 1; i++)
        {
            var price = prices[i];
            var (highestPrice, highestPriceDay) = highest[i+1];

            var highestProfitForBuyDay = highestPrice - price;
            if(highestProfitForBuyDay > mostProfit)
            {
                mostProfit = highestProfitForBuyDay;
                _buyDay = i;
                _sellDay = highestPriceDay;
            }
        }
    }

    public int GetBuyDay()
    {
        return _buyDay;
    }

    public int GetSellDay()
    {
        return _sellDay;
    }

    private (IList<int> Prices, Dictionary<int, (int Price, int Day)> HightestPrice) PreparePrices(APICaller api)
    {
        var days = api.GetDays();
        var prices = new int[days];
        var highest = new Dictionary<int, (int Price, int Day)>(days);

        var highestPrice = int.MinValue;
        var highestPriceDay = 0;
        for (int i = days; i > 0; i--)
        {
            var day = i - 1;
            var price = api.GetPriceForDay(day);
            prices[day] = price;

            if (price > highestPrice)
            {
                highestPrice = price;
                highestPriceDay = day;
            }
            highest[day] = (highestPrice, highestPriceDay);
        }

        return (prices, highest);
    }
}
