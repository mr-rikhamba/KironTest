Create or Alter PROCEDURE SP_LoginUser
    @username VARCHAR(50),
    @password VARCHAR(100)
    AS
    BEGIN
        BEGIN TRY

            Create TABLE #tmpUser (
                UserId bigint,
                Username VARCHAR(50),
                DateAdded DATETIME2,
                FirstName VARCHAR(50),
                LastName VARCHAR(50),
                IsActive bit
            )

            Declare @hashedPassword VARCHAR(100) = CONVERT(NVARCHAR(64), HASHBYTES('SHA2_512', @password), 2);

             INSERT into #tmpUser
             Select UserId, Username, DateAdded, FirstName, LastName, IsActive
             from [Users] 
             where Username = @username AND LoginPassword = @hashedPassword
            IF Exists(Select 1 From #tmpUser )
            BEGIN
                SELECT * from #tmpUser
            Drop Table #tmpUser;

            END
            ELSE
                THROW 500000, 'Invalid credentials entered.', 1;

        End TRY
        Begin CATCH
            Drop Table #tmpUser;
            DECLARE @ErrorMessage VARCHAR(4000), @ErrorSeverity INT, @ErrorState INT;
            SELECT 
                @ErrorMessage = ERROR_MESSAGE(), 
                @ErrorSeverity = ERROR_SEVERITY(), 
                @ErrorState = ERROR_STATE();
            RAISERROR (@ErrorMessage, @ErrorSeverity, @ErrorState);
        End CATCH
    END

--  exec  SP_LoginUser 'Tiyi', 'Pass'