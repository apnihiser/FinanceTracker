CREATE PROCEDURE [dbo].[spAccount_Delete]
	@Id int
AS
BEGIN
	DELETE
	FROM dbo.Account
	WHERE Id = @Id;
END