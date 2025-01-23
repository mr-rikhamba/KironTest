CREATE or Alter PROCEDURE SP_CreateRegions
    @regions RegionType READONLY
AS
    BEGIN
        BEGIN TRY
            
            INSERT into Regions (RegionName)
            Select distinct r.RegionName
            From @regions r
            where Not Exists(
                Select 1 from Regions Where RegionName = r.RegionName
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