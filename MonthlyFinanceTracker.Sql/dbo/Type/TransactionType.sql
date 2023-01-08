CREATE TYPE [dbo].TransactionType AS TABLE
(
	[Id] INT NOT NULL, 
    [AccountId] INT NOT NULL, 
    [PayeeId] INT NOT NULL, 
    [TransactionReason] NVARCHAR(20),
    [Type] NVARCHAR(10),
    [Amount] MONEY NOT NULL, 
    [DueDate] DATETIME2 NOT NULL, 
    [Status] NVARCHAR(50) NOT NULL
)
