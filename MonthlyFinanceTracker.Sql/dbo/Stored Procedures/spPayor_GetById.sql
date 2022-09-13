CREATE PROCEDURE [dbo].[spPayor_GetById]
	@Id int

AS
BEGIN
	SELECT [Id], [FirstName], [LastName]
	FROM dbo.Payor
	WHERE Id = @Id
END
