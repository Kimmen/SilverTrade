using BenchmarkDotNet.Attributes;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kimmen.SilverTrade.Benchmark;

[MemoryDiagnoser]
public class ThousandsBenchmark
{
    private readonly Random random = new(1000);
    private APICaller _api;

    [Params(100, 500, 1000, 5000, 10_000)]
    public int Days;

    [GlobalSetup]
    public void Setup()
    {
        var prices = Enumerable
            .Range(0, Days)
            .Select(_ => random.Next(0, 1000)).ToArray();

        _api = new APICaller(prices);
    }

    [Benchmark]
    public void NPassForwardTrade()
    {
        var trader = new NPassForwardTrade(_api);
        trader.GetBuyDay();
        trader.GetSellDay();
    }

    [Benchmark]
    public void TwoPassBackForwardTrade()
    {
        var trader = new TwoPassBackForwardTrade(_api);
        trader.GetBuyDay();
        trader.GetSellDay();
    }

    [Benchmark]
    public void OnePassBackwardsTrade()
    {
        var trader = new OnePassBackwardsTrade(_api);
        trader.GetBuyDay();
        trader.GetSellDay();
    }
}