CREATE PROCEDURE [dbo].[spPayor_GetAll]
AS
BEGIN
	SELECT [Id], [FirstName], [LastName]
	FROM dbo.Payor
END
