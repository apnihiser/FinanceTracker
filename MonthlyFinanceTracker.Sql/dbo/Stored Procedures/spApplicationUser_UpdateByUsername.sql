CREATE PROCEDURE [dbo].[spApplicationUser_UpdateByUsername]
	@Username VARCHAR(20),
	@NormalizedUsername VARCHAR(20),
	@Email VARCHAR(30),
	@NormalizedEmail VARCHAR(30),
	@Fullname VARCHAR(30),
	@PasswordHash VARCHAR(MAX)

AS
BEGIN
	UPDATE dbo.ApplicationUser
	SET Username = @Username,
		NormalizedUsername = @NormalizedUsername,
		Email = @Email,
		NormalizedEmail = @NormalizedEmail,
		Fullname = @Fullname
END
