using System;

namespace KironTest.Logic.Models;

public class ExternalServiceConfigs
{
    public required string BankHolidayUrl { get; set; }
    public required string CoinStatsUrl { get; set; }
    public required string CoinStatsApiKey { get; set; }
}
