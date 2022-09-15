CREATE PROCEDURE [dbo].[spApplicationRole_GetById]
	@Id int
AS
BEGIN
	SELECT *
	FROM [dbo].[ApplicationRole]
	WHERE Id = @Id
END
