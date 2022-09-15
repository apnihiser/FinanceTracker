CREATE PROCEDURE [dbo].[spApplicationUser_DeleteById]
	@Id int
AS
BEGIN
	DELETE
	FROM dbo.ApplicationUser
	WHERE ApplicationUserId = @Id;
END
