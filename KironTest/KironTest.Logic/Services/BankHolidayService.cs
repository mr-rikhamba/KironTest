using System;
using System.Data;
using System.Text.Json;
using Dapper;
using KironTest.Logic.Contracts;
using KironTest.Logic.Helpers;
using KironTest.Logic.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace KironTest.Logic.Services;

public class BankHolidayService(ILogger<BankHolidayService> _logger, IHttpClientFactory _httpClientFactory, IRepositoryContract _repositoryService, IOptions<ExternalServiceConfigs> configs) : IBankHolidayContract
{
    private readonly ExternalServiceConfigs _externalServiceConfigs = configs.Value;
    public async Task UpdateHolidayData()
    {
        using (var httpClient = _httpClientFactory.CreateClient())
        {
            httpClient.BaseAddress = new Uri(_externalServiceConfigs.BankHolidayUrl);
            var response = await httpClient.GetAsync("bank-holidays.json");
            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true // Enables case-insensitive matching
            };

            var dataList = JsonSerializer.Deserialize<Dictionary<string, RegionStorageModel>>(json, options);

            var holidayTable = ConvertToHolidayDataTable(dataList);
            var regionTable = ConvertToRegionDataTable(dataList);
            var regionHolidayTable = ConvertToRegionHolidayMappingDataTable(dataList);

            await AddBankHolidaysAsync(holidayTable, "SP_AddBankHolidays", "holidays", "HolidayType");
            await AddBankHolidaysAsync(regionTable, "SP_CreateRegions", "regions", "RegionType");
            await AddBankHolidaysAsync(regionHolidayTable, "SP_HolidayMappings", "regionHolidays", "RegionHolidayType");
        }
    }



    public async Task<BaseResponseModel<List<RegionModel>>> GetRegions()
    {
        try
        {
            var result = await _repositoryService.Execute<RegionModel>("SP_GetRegions");
            return new BaseResponseModel<List<RegionModel>>
            {
                ResponseData = result
            };
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "SQL Exception.");
            return new BaseResponseModel<List<RegionModel>> { IsSuccessful = false, ResponseMessage = ex.Message };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown Error.");
            throw;
        }
    }

    public async Task<BaseResponseModel<List<RegionHolidayModel>>> GetRegionHolidays(int regionId)
    {
        try
        {
            if (regionId < 1)
            {
                throw new ArgumentException("Please use a valid region id.");
            }
            var parameters = new DynamicParameters();
            parameters.Add("@regionId", regionId, DbType.Int32);
            var result = await _repositoryService.Execute<RegionHolidayModel>("SP_HolidayByRegion", parameters);
            return new BaseResponseModel<List<RegionHolidayModel>>
            {
                ResponseData = result
            };
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "SQL Exception.");
            return new BaseResponseModel<List<RegionHolidayModel>> { IsSuccessful = false, ResponseMessage = ex.Message };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unknown Error.");
            throw;
        }
    }
    #region Local Methods

    private async Task AddBankHolidaysAsync(DataTable dTbl, string spName, string paramName, string tvpName)
    {
        var parameters = new DynamicParameters();
        parameters.Add($"@{paramName}", dTbl.AsTableValuedParameter(tvpName));
        await _repositoryService.ExecuteTvp(dTbl, spName, parameters);
    }

    private static DataTable ConvertToHolidayDataTable(Dictionary<string, RegionStorageModel> data)
    {
        var table = new DataTable();
        table.Columns.Add("Title", typeof(string));
        table.Columns.Add("HolidayDate", typeof(DateTime));
        table.Columns.Add("Notes", typeof(string));
        table.Columns.Add("Bunting", typeof(bool));

        var holidays = data.SelectMany(r => r.Value.Events).DistinctBy(h => new { h.Title, h.Date });

        foreach (var holiday in holidays)
        {
            table.Rows.Add(holiday.Title, holiday.Date, holiday.Notes, holiday.Bunting);
        }

        return table;
    }
    private static DataTable ConvertToRegionDataTable(Dictionary<string, RegionStorageModel> data)
    {
        var table = new DataTable();
        table.Columns.Add("Name", typeof(string));

        foreach (var region in data.Keys)
        {
            table.Rows.Add(region);
        }

        return table;
    }
    private static DataTable ConvertToRegionHolidayMappingDataTable(Dictionary<string, RegionStorageModel> data)
    {
        var table = new DataTable();
        table.Columns.Add("RegionName", typeof(string));
        table.Columns.Add("HolidayName", typeof(string));

        foreach (var region in data)
        {
            foreach (var holiday in region.Value.Events)
            {
                table.Rows.Add(region.Key, holiday.Title);
            }
        }

        return table;
    }
    #endregion

}
