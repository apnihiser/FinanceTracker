CREATE PROCEDURE [dbo].[spAccount_Update]
	@Id int,
	@Title nvarchar(50),
	@Description nvarchar(100),
	@Type nvarchar(50),
	@Balance money,
	@HolderId int
AS
BEGIN
	Update dbo.Account
	SET [Title] = @Title
	   ,[Description] = @Description
	   ,[Type] = @Type
	   ,[Balance] = @Balance
	   ,[HolderId] = @HolderId
	WHERE Id = @Id;
END
