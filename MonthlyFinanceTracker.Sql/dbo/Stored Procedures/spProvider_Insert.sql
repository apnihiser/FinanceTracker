CREATE PROCEDURE [dbo].[spProvider_Insert]
	@Title varchar(50),
	@Service varchar(100),
	@URL varchar(300),
	@PayorId int,
	@Id int output
AS
BEGIN
	INSERT INTO dbo.Provider (Title, Service, URL, PayorId)
	VALUES (@Title,@Service,@URL,@PayorId)

	set @Id = SCOPE_IDENTITY();
END
