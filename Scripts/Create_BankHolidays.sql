CREATE Table Holidays(
    HolidayId int IDENTITY(1,1) PRIMARY Key,
    Title VARCHAR(500) not NULL,
    HolidayDate DATE not NULL,
    Notes VARCHAR(500) not NULL,
    Bunting bit not NULL
)
CREATE Table Regions(
    RegionId int IDENTITY(1,1) PRIMARY Key,
    RegionName VARCHAR(50) not NULL,
)

CREATE Table RegionHolidays(
    RegionHolidayId int IDENTITY(1,1) PRIMARY Key,
    RegionId int FOREIGN Key REFERENCES Regions(RegionId),
    HolidayId int FOREIGN Key REFERENCES Holidays(HolidayId)
)

--Types
CREATE Type HolidayType as Table(
    Title VARCHAR(500),
    HolidayDate DATE,
    Notes VARCHAR(500),
    Bunting bit
)
CREATE Type RegionType as Table(
    RegionName VARCHAR(50)
)

CREATE Type RegionHolidayType as Table(
    RegionName VARCHAR(500),
    HolidayName VARCHAR(500)
)