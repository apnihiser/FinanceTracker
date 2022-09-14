CREATE PROCEDURE [dbo].[spApplicationUser_DeleteByUsername]
	@NormalizedUsername varchar(20)
AS
BEGIN
	DELETE
	FROM dbo.ApplicationUser
	WHERE NormalizedUsername = @NormalizedUsername
END
