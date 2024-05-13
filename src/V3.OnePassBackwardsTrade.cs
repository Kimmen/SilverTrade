namespace Kimmen.SilverTrade;

public class OnePassBackwardsTrade : ISilverTrade
{
    private int _buyDay;
    private int _sellDay; 

    public OnePassBackwardsTrade(APICaller api)
    {
        var days = api.GetDays();
       
        var highestPrice = int.MinValue;
        var highestPriceDay = int.MinValue;
        var mostProfit = int.MinValue;

        //Go backwards instead of forward
        for (int i = days; i > 0; i--)
        {
            var day = i - 1;
            var price = api.GetPriceForDay(day);

            //Practically prevent calculating profit on the first loop.
            if(day < highestPriceDay)
            {
                //Determine profit.
                var profit = highestPrice - price;
                if(profit > mostProfit)
                {
                    mostProfit = profit;
                    _buyDay = day;
                    _sellDay = highestPriceDay;
                }
            }

            //Keep track of the day with highest sell price
            if (price > highestPrice)
            {
                highestPrice = price;
                highestPriceDay = day;
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
