CREATE PROCEDURE [dbo].[spApplicationUser_Insert]
	@ApplicationUser ApplicationUserType READONLY
AS
BEGIN
	INSERT INTO [dbo].[ApplicationUser] (
										 [Username],
										 [NormalizedUsername],
										 [Email],
										 [NormalizedEmail],
										 [Fullname],
										 [PasswordHash])
	SELECT [Username],
		   [NormalizedUsername],
		   [Email],
		   [NormalizedEmail],
		   [Fullname],
		   [PasswordHash]
	FROM @ApplicationUser;

	SELECT CAST(SCOPE_IDENTITY() AS INT);
END
