namespace Kimmen.SilverTrade;

public class APICaller(IList<int> _prices)
{

    public int GetDays()
    {
        return _prices.Count;
    }

    public int GetPriceForDay(int day)
    {
        return _prices[day];
    }
}