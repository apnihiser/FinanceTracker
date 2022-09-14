CREATE PROCEDURE [dbo].[spProvider_GetById]
	@Id int
AS
BEGIN
	SELECT [Id], [Title], [Service], [URL], [UserId]
	FROM dbo.Provider
	WHERE Id = @Id;
END
