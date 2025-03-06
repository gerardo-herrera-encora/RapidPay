using RapidPay.Application.Interfaces;

namespace RapidPay.Application.Services;

public class UniversalFeesExchangeExchangeService : IUniversalFeesExchangeService
{
    private readonly Timer _timer;
    private decimal RecentRandomDecimal { get; set; } = 1;
    private decimal LastFeeAmount { get; set; } = 1;

    public UniversalFeesExchangeExchangeService()
    {
        _timer = new Timer(GetNextRandom, null, TimeSpan.Zero, TimeSpan.FromHours(1));
    }
    
    private void GetNextRandom(object? state)
    {
        var random = new Random();
        RecentRandomDecimal = (decimal)random.NextDouble() * 2;
    }
    
    public decimal CalculateFee()
    {
        LastFeeAmount *= RecentRandomDecimal;
        return LastFeeAmount;
    }
}
