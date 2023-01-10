CREATE PROCEDURE [dbo].[spTransaction_DeleteByProviderId]
	@ProviderId int
AS
BEGIN
	DELETE FROM [dbo].[Transaction]
	WHERE [PayeeId] = @ProviderId;
END
