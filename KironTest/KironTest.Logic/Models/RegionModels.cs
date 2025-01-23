using System;

namespace KironTest.Logic.Models;

public class RegionStorageModel
{
    public string Division { get; set; }
    public List<HolidayModel> Events { get; set; }
}

public class HolidayModel
{
    public string Title { get; set; }
    public string Date { get; set; }
    public string Notes { get; set; }
    public bool Bunting { get; set; }
}

public class RegionModel
{
    public int RegionId { get; set; }
    public string RegionName { get; set; }
}
public class RegionHolidayModel
{
    public int RegionId { get; set; }
    public int HolidayId { get; set; }
    public string Title { get; set; }
    public string HolidayDate { get; set; }
    public string Notes { get; set; }
    public bool Bunting { get; set; }
}