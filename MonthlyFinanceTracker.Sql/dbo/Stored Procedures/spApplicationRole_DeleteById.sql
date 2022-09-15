CREATE PROCEDURE [dbo].[spApplicationRole_DeleteById]
	@Id int
AS
BEGIN
	DELETE [dbo].[ApplicationRole]
	WHERE [Id] = @Id
END