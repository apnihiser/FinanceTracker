CREATE PROCEDURE [dbo].[spAccount_Insert]
	@Id int out,
	@Title nvarchar(50),
	@Description nvarchar(100),
	@Type nvarchar(50),
	@Balance money,
	@HolderId int
AS
BEGIN
	INSERT INTO dbo.Account ([Title],[Description],[Type],[Balance],[HolderId])
	VALUES (@Title,@Description,@Type,@Balance,@HolderId)

	SET @Id = SCOPE_IDENTITY();
END
