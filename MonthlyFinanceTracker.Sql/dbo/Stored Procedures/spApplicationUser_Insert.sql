CREATE PROCEDURE [dbo].[spApplicationUser_Insert]
	@ApplicationUser ApplicationUserType READONLY
AS
BEGIN
	INSERT INTO [dbo].[ApplicationUser] ([Fullname],
										 [Username],
										 [NormalizedUsername],
										 [Email],
										 [NormalizedEmail],
										 [EmailConfirmed],
										 [PasswordHash],
										 [PhoneNumber],
										 [PhoneNumberConfirmed],
										 [TwoFactorEnabled])
	SELECT [Fullname],
		   [Username],
		   [NormalizedUsername],
		   [Email],
		   [NormalizedEmail],
		   [EmailConfirmed],
		   [PasswordHash],
		   [PhoneNumber],
		   [PhoneNumberConfirmed],
		   [TwoFactorEnabled]
	FROM @ApplicationUser;

	SELECT CAST(SCOPE_IDENTITY() AS INT);
END
