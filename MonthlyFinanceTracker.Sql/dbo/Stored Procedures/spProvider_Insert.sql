CREATE PROCEDURE [dbo].[spProvider_Insert]
	@Title varchar(50),
	@Service varchar(100),
	@URL varchar(300),
	@UserId int,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.Provider ([Title], [Service], [URL], [UserId])
	VALUES (@Title,@Service,@URL,@UserId)

	set @Id = SCOPE_IDENTITY();
END
