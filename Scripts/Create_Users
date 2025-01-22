CREATE TABLE Users(
    UserId bigint    IDENTITY(1,1) Not Null PRIMARY Key,
    Username VARCHAR(50) Not Null,
    LoginPassword VARCHAR(100) Not Null,
    DateAdded DATETIME2 Not null DEFAULT( GetDate()),
    FirstName VARCHAR(50)  Null,
    LastName VARCHAR(50)  Null,
    IsActive bit null DEFAULT(1)
)

--Drop Table Users