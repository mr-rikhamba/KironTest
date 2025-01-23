Create or Alter PROCEDURE SP_GetRegions
    AS
    BEGIN
        BEGIN TRY
        Select [RegionId]
      ,[RegionName]
        From Regions
           
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
