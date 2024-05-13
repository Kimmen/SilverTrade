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

        //Start with the last day and go backwards, so we can easily keep track of the highest sell price
        //and calculate max profit as we go
        for (int i = days; i > 0; i--)
        {
            var day = i - 1;
            var price = api.GetPriceForDay(day);

            //Practically prevent calculating profit on the first loop.
            if(day < highestPriceDay)
            {
                //Determine profit with the highest sell price found yet.
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
}
