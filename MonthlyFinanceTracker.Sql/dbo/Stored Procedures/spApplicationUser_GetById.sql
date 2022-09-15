CREATE PROCEDURE [dbo].[spApplicationUser_GetById]
	@Id int
AS
BEGIN
	SELECT *
	FROM [dbo].[ApplicationUser]
	WHERE ApplicationUserId = @Id;
END
