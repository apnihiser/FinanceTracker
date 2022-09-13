CREATE PROCEDURE [dbo].[spProvider_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM dbo.Provider
	WHERE Id = @Id;
END
