CREATE PROCEDURE [dbo].[spAccount_GetAll]
AS
BEGIN
	SELECT [Id], [Title], [Description], [Type], [Balance], [HolderId]
	FROM dbo.Account
END
