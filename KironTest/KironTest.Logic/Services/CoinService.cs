using System;
using System.Text.Json;
using KironTest.Logic.Contracts;
using KironTest.Logic.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KironTest.Logic.Services;

public class CoinService(IHttpClientFactory _httpClientFactory, IOptions<ExternalServiceConfigs> configs) : ICoinContract
{
    private readonly ExternalServiceConfigs _externalServiceConfigs = configs.Value;
    public async Task<CoinModel> GetCoins()
    {
        using (var httpClient = _httpClientFactory.CreateClient())
        {
            httpClient.BaseAddress = new Uri(_externalServiceConfigs.CoinStatsUrl);
            httpClient.DefaultRequestHeaders.Add("X-API-KEY", _externalServiceConfigs.CoinStatsApiKey);
            var response = await httpClient.GetAsync("coins");
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Enables case-insensitive matching
            };

            var bitCoin = JsonSerializer.Deserialize<CoinModel>(json, options);
            return bitCoin;
        }
    }
}
