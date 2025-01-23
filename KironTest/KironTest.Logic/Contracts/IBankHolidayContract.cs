using System;
using KironTest.Logic.Models;

namespace KironTest.Logic.Contracts;

public interface IBankHolidayContract
{
    Task UpdateHolidayData();
    Task<BaseResponseModel<List<RegionModel>>> GetRegions();
    Task<BaseResponseModel<List<RegionHolidayModel>>> GetRegionHolidays(int regionId);

}
