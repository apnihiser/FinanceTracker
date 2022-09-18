CREATE PROCEDURE [dbo].[spAccount_Update]
	@Account AccountType READONLY
AS
BEGIN
	Update [dbo].[Account]
	SET [Title] = a.Title
	   ,[Description] = a.Description
	   ,[Type] = a.Type
	   ,[Balance] = a.Balance
	   ,[ApplicationUserId] = a.ApplicationUserId
	FROM @Account a
	WHERE [Account].[Id] = a.Id;
END
