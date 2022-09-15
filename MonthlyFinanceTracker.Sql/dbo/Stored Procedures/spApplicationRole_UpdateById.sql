CREATE PROCEDURE [dbo].[spApplicationRole_UpdateById]
	@Id int,
	@Name nvarchar(32),
	@NormalizedName nvarchar(32)
AS
BEGIN
	UPDATE [dbo].[ApplicationRole]
	SET [Name] = @Name, [NormalizedName] = @NormalizedName
	WHERE [Id] = @Id
END
