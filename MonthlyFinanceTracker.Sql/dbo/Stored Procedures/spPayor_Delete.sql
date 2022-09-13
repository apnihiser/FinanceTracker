CREATE PROCEDURE [dbo].[spPayor_Delete]
	@Id int
AS
BEGIN
	DELETE 
	FROM dbo.Payor
	WHERE Id = @Id
END
