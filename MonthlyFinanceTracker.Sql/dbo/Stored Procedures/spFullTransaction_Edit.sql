CREATE PROCEDURE [dbo].[spFullTransaction_Edit]
	@TransactionType TransactionType READONLY
AS
BEGIN
	Update [dbo].[Transaction]
	SET [AccountId] = t1.[AccountId]
	   ,[PayeeId] = t1.[PayeeId]
	   ,[TransactionReason] = t1.[TransactionReason]
	   ,[Type] = t1.[Type]
	   ,[Amount] = t1.[Amount]
	   ,[DueDate] = t1.[DueDate]
	   ,[Status] = t1.[Status]
	FROM @TransactionType t1
	WHERE [dbo].[Transaction].Id = t1.Id
END
