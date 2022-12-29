CREATE PROCEDURE [dbo].[spAccount_GetById]
	@Id int
AS
BEGIN
	SELECT TOP 1 [Id], [Title], [Description], [Type], [Balance], [ApplicationUserId]
	FROM [dbo].[Account]
	WHERE Id = @Id
END
