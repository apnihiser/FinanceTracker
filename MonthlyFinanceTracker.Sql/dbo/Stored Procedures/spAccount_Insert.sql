CREATE PROCEDURE [dbo].[spAccount_Insert]
	@Id int out,
	@Title nvarchar(50),
	@Description nvarchar(100),
	@Type nvarchar(50),
	@Balance money,
	@ApplicationUserId int
AS
BEGIN
	INSERT INTO dbo.Account ([Title],[Description],[Type],[Balance],[ApplicationUserId])
	VALUES (@Title,@Description,@Type,@Balance,@ApplicationUserId)

	SET @Id = SCOPE_IDENTITY();
END
