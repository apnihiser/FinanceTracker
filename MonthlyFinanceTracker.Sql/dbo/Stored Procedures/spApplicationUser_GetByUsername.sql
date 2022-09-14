CREATE PROCEDURE [dbo].[spApplicationUser_GetByUsername]
	@NormalizedUsername VARCHAR(20)
AS
BEGIN
	SELECT [ApplicationUserId], 
		   [Username],
		   [NormalizedUsername], 
		   [Email],
		   [NormalizedEmail],
		   [Fullname],
		   [PasswordHash]
	FROM ApplicationUser
	WHERE [NormalizedUsername] = @NormalizedUsername
END
