CREATE PROCEDURE [dbo].[spAccount_GetById]
	@Id int
AS
BEGIN
	SELECT [Id], [Title], [Description], [Type], [Balance], [ApplicationUserId]
	FROM [dbo].[Account]
	WHERE Id = @Id
END
