CREATE PROCEDURE [dbo].[spAccount_GetAccountsByUserId]
	@ApplicationUserId int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Title], [Description], [Type], [Balance], [ApplicationUserId]
	FROM dbo.Account a
	WHERE a.Id = @ApplicationUserId;
END