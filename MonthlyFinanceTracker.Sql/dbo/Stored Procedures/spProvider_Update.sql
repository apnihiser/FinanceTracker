CREATE PROCEDURE [dbo].[spProvider_Update]
	@Id int,
	@Title varchar(50),
	@Service varchar(100),
	@URL varchar(300)

AS
BEGIN
	Update dbo.Provider
	SET [Title] = @Title, [Service] = @Service,	[URL] = @URL
	WHERE Id = @Id
END
