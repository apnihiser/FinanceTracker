CREATE PROCEDURE [dbo].[spApplicationUser_FindByNormalizedEmail]
	@NormalizedEmail nvarchar(30)
AS
BEGIN
	SELECT *
	FROM [dbo].[ApplicationUser]
	WHERE NormalizedEmail = @NormalizedEmail;
END
