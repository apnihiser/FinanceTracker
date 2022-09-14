CREATE PROCEDURE [dbo].[spApplicationUser_GetByUsername]
	@NormalizedUsername VARCHAR(20)
AS
BEGIN
	SELECT [ApplicationUserId],
		   [Fullname],
		   [UserName],
		   [NormalizedUserName],
		   [Email],
		   [NormalizedEmail],
		   [EmailConfirmed],
		   [PasswordHash],
		   [PhoneNumber],
		   [PhoneNumberConfirmed],
		   [TwoFactorEnabled]
	FROM ApplicationUser
	WHERE [NormalizedUsername] = @NormalizedUsername
END
