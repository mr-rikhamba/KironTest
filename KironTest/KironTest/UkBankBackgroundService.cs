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
                await _bankHolidayContract.UpdateHolidayData();
            }
            await Task.Delay(TimeSpan.FromSeconds(5));
        }
    }
}
