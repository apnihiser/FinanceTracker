CREATE PROCEDURE [dbo].[spProvider_GetAll]
	@ApplicationUserId int
AS
BEGIN
	SELECT [Id], [Title], [Service], [URL], [UserId]
	FROM dbo.Provider as p
	WHERE p.UserId = @ApplicationUserId
END
