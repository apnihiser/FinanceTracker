CREATE TYPE [dbo].[AccountType] AS TABLE
(
    [Id] INT,
    [Title] NVARCHAR(50) NOT NULL, 
    [Description] NVARCHAR(100) NOT NULL, 
    [Type] NVARCHAR(50) NOT NULL, 
    [Balance] MONEY NOT NULL, 
    [ApplicationUserId] INT NOT NULL
);
