CREATE or Alter PROCEDURE SP_AddBankHolidays
    @holidays HolidayType READONLY
AS
    BEGIN
        BEGIN TRY
            
            INSERT into Holidays (Title, HolidayDate, Notes, Bunting)
            Select distinct h.Title, h.HolidayDate, h.Notes, h.Bunting
            From @holidays h
            where Not Exists(
                Select 1 from Holidays Where Title = h.Title and HolidayDate = h.HolidayDate
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