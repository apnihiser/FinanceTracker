CREATE TABLE [dbo].[Transaction]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1), 
    [AccountId] INT NOT NULL, 
    [PayeeId] INT NOT NULL, 
    [TransactionReason] NVARCHAR(20) NOT NULL,
    [Type] NVARCHAR(10) NOT NULL,
    [Amount] MONEY NOT NULL, 
    [DueDate] DATETIME2 NOT NULL, 
    [Status] NVARCHAR(50) NOT NULL
)
