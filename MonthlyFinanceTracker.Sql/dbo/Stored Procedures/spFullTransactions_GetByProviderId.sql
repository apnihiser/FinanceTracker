CREATE PROCEDURE [dbo].[spFullTransactions_GetByProviderId]
	@ProviderId int
AS
BEGIN
	SELECT [Id], [AccountId], [PayeeId], [TransactionReason], [Type], [Amount], [DueDate], [Status]
	FROM [dbo].[Transaction]
	WHERE PayeeId = @ProviderId;
END
