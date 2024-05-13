using static System.Formats.Asn1.AsnWriter;

namespace Kimmen.SilverTrade;

public class TwoPassBackForwardTrade : ISilverTrade
{
    private int _buyDay;
    private int _sellDay; 

    public TwoPassBackForwardTrade(APICaller api)
    {
        //Pre-evaluate the highest sell price each day can reach going forward, and store it in a dictionary to be used later.
        var (prices, highest) = this.PrepareSellPrices(api);
        var mostProfit = int.MinValue;

        //Start from the first day and calculate profit by look-up the what the highest sell price would be for the given day.
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

    private (IList<int> Prices, Dictionary<int, (int Price, int Day)> HightestPrice) PrepareSellPrices(APICaller api)
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
