Create or Alter PROCEDURE SP_HolidayByRegion
@regionId int
    AS
    BEGIN
        BEGIN TRY
      
      Select 
        rh.RegionId,
        h.*
        From RegionHolidays rh 
        Inner Join Holidays h on h.HolidayId = rh.HolidayId
        where rh.RegionId = @regionId
           
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

    --SP_GetRegions
   