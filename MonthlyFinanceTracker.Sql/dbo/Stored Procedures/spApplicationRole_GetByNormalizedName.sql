CREATE PROCEDURE [dbo].[spApplicationRole_GetByNormalizedUsername]
	@NormalizedName varchar(20)
AS
BEGIN
	SELECT *
	FROM [dbo].[ApplicationRole]
	WHERE NormalizedName = @NormalizedName;
END
