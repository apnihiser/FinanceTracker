CREATE PROCEDURE [dbo].[spAccount_GetAll]
AS
BEGIN
	SELECT [Id], [Title], [Description], [Type], [Balance], [ApplicationUserId]
	FROM [dbo].[Account] as a
END
