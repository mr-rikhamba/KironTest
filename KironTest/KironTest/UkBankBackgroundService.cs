using System;
using KironTest.Logic.Contracts;
using KironTest.Logic.Helpers;

namespace KironTest;

public class UkBankBackgroundService(ILogger<UkBankBackgroundService> _logger, IBankHolidayContract _bankHolidayContract, BankServiceManager _bankServiceManager) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            if (_bankServiceManager.IsServiceEnabled)
            {
                _logger.LogInformation("Starting data retrieval.");
                await _bankHolidayContract.UpdateHolidayData();
                _logger.LogInformation("Data Updated.");
            }
            await Task.Delay(TimeSpan.FromMinutes(1));
        }
    }
}
