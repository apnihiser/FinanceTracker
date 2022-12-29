CREATE PROCEDURE [dbo].[spAccount_GetByAccountId]
	@id int
AS
BEGIN
	SET NOCOUNT ON;

	SELECT [Id], [Title], [Description], [Type], [Balance], [ApplicationUserId]
	FROM dbo.Account a
	WHERE a.Id = @id;
END
