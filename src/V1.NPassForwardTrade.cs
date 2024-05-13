namespace Kimmen.SilverTrade;

public class NPassForwardTrade : ISilverTrade
{
    private int _buyDay = 0;
    private int _sellDay = 0;

    public NPassForwardTrade(APICaller api)
    {
        var prices = GetPrices(api);

        var mostProfit = int.MinValue;
        for(int day = 0; day < prices.Count; day++)
        {
            var price = prices[day];
            
            for(int c = day + 1; c < prices.Count; c++)
            {
                var currentPrice = prices[c];
                var profit = currentPrice - price;

                if(mostProfit < profit)
                {
                    mostProfit = profit;
                    _buyDay = day;
                    _sellDay = c;
                }
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

    private IList<int> GetPrices(APICaller api)
    {
        var days = api.GetDays();
        var prices = new List<int>(days);

        for (var d = 0; d < days; d++)
        {
            prices.Add(api.GetPriceForDay(d));
        }

        return prices;
    }
}
