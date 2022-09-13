CREATE PROCEDURE [dbo].[spPayor_Insert]
	@FirstName varchar(50),
	@LastName varchar(50),
	@Id int out
AS
BEGIN
	INSERT INTO dbo.Payor (FirstName,LastName)
	VALUES (@FirstName,@LastName)

	set @Id = SCOPE_IDENTITY();
END
