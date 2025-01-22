Create or Alter PROCEDURE SP_CreateUser
    @username VARCHAR(50),
    @password VARCHAR(100),
    @firstName VARCHAR(50),
    @lastName VARCHAR(50)
    AS
    BEGIN
        BEGIN TRY
            IF NOT EXISTS(Select Username From [Users] where Username = @Username)
            BEGIN

            Declare @hashedPassword VARCHAR(100) = CONVERT(VARCHAR(64), HASHBYTES('SHA2_512', @password), 2);

            INSERT INTO [Users] (Username, LoginPassword, FirstName, LastName)
            VALUES (@Username, @hashedPassword, @firstName, @lastName);
            
            END ELSE
                  THROW 500000, 'Username already exists, please use another one.', 1;
               
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

 -- exec  SP_CreateUser 'Tiyi', 'Pass', 'Tiyiselani', 'Rikhamba'