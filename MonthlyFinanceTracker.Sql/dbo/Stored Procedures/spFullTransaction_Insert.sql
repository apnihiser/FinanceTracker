CREATE PROCEDURE [dbo].[spFullTransaction_Insert]
	@TransactionType TransactionType READONLY
AS
BEGIN
	INSERT INTO [dbo].[Transaction]
		([AccountId]
		,[PayeeId]
		,[Amount]
		,[DueDate]
		,[Status])
	SELECT
		[AccountId]
		,[PayeeId]
		,[Amount]
		,[DueDate]
		,[Status]
	FROM @TransactionType;

	SELECT CAST(SCOPE_IDENTITY() as INT);
END
