CREATE PROCEDURE [dbo].[spApplicationRole_Insert]
	@Id int out,
	@Name nvarchar(32),
	@NormalizedName nvarchar(32)
AS
BEGIN
	INSERT INTO [dbo].[ApplicationRole] ([Name], [NormalizedName])
	VALUES (@Name, @NormalizedName);

	SET @Id = SCOPE_IDENTITY();
END
