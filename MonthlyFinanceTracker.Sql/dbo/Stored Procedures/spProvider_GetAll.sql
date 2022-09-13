CREATE PROCEDURE [dbo].[spProvider_GetAll]
AS
BEGIN
	SELECT [Id], [Title], [Service], [URL], [PayorId]
	FROM dbo.Provider
END
