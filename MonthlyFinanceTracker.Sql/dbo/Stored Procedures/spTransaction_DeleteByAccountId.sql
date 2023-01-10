CREATE PROCEDURE [dbo].[spTransaction_DeleteByAccountId]
	@AccountId int
AS
BEGIN
	DELETE FROM [dbo].[Transaction]
	WHERE [AccountId] = @AccountId;
END
