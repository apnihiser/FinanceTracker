CREATE TABLE [dbo].[Account]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(100) NOT NULL, 
    [Type] NVARCHAR(50) NOT NULL, 
    [Balance] MONEY NOT NULL, 
    [UserId] INT NOT NULL
)
