CREATE PROCEDURE [dbo].[spApplicationUser_UpdateById]
	    @ApplicationUserId INT,
        @Fullname NVARCHAR(30),
        @Username NVARCHAR(20),
        @NormalizedUsername NVARCHAR(20),
        @Email NVARCHAR(30),
        @NormalizedEmail NVARCHAR(30),
        @EmailConfirmed BIT,
        @PasswordHash NVARCHAR(MAX),
        @PhoneNumber NVARCHAR(20),
        @PhoneNumberConfirmed BIT,
        @TwoFactorEnabled BIT
AS
BEGIN
	UPDATE dbo.ApplicationUser
	SET 
        [Fullname] = @Fullname,
        [Username] = @Username,
        [NormalizedUsername] = @NormalizedUsername,
        [Email] = @Email,
        [NormalizedEmail] = @NormalizedEmail,
        [EmailConfirmed] = @EmailConfirmed,
        [PasswordHash] = @PasswordHash,
        [PhoneNumber] = @PhoneNumber,
        [PhoneNumberConfirmed] = @PhoneNumberConfirmed,
        [TwoFactorEnabled] = @TwoFactorEnabled
    WHERE ApplicationUserId = @ApplicationUserId
END
