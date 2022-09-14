CREATE PROCEDURE [dbo].[spProvider_GetAll]
AS
BEGIN
	SELECT [Id], [Title], [Service], [URL], [UserId]
	FROM dbo.Provider
END
