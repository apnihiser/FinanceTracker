CREATE PROCEDURE [dbo].[spPayor_Update]
	@Id int,
	@FirstName varchar(50),
	@LastName varchar(50)
AS
BEGIN
	UPDATE dbo.Payor
	SET FirstName = @FirstName,
	LastName = @LastName
	WHERE Id = @Id
END
