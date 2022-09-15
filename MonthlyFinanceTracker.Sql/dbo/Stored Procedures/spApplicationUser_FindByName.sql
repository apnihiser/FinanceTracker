CREATE PROCEDURE [dbo].[spApplicationUser_FindByName]
	@NormalizedUsername NVARCHAR(20) 
AS
BEGIN
	SELECT *
	FROM [dbo].[ApplicationUser]
	WHERE NormalizedUsername = @NormalizedUsername;
END