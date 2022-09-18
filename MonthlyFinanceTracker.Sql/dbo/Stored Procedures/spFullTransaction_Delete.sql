CREATE PROCEDURE [dbo].[spFullTransaction_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM [dbo].[Transaction]
	WHERE Id = @Id;
END
