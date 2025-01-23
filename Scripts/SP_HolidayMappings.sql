CREATE or Alter PROCEDURE SP_HolidayMappings
    @regionHolidays RegionHolidayType READONLY
AS
    BEGIN
        BEGIN TRY
            
            INSERT into RegionHolidays (RegionId, HolidayId)
            Select distinct r.RegionId, h.HolidayId
            From @regionHolidays rh
            INNER Join Regions r on r.RegionName = rh.RegionName
            INNER Join Holidays h on h.Title = rh.HolidayName

            where Not Exists(
                Select 1 from RegionHolidays Where RegionId = r.RegionId and HolidayId = h.HolidayId
            )
            
        End TRY
        Begin CATCH
            DECLARE @ErrorMessage VARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
            SELECT 
                @ErrorMessage = ERROR_MESSAGE(), 
                @ErrorSeverity = ERROR_SEVERITY(), 
                @ErrorState = ERROR_STATE();
            RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        End CATCH
    END