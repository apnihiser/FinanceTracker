CREATE PROCEDURE [dbo].[spAccountCostByName_GetById]
	@ApplicationUserId int
AS
BEGIN
	SELECT SUM([att].[Balance]) as Sum, [att].[Title]
	FROM (
		SELECT *
		FROM [dbo].[Account]
		WHERE ApplicationUserId = @ApplicationUserId ) as att
	GROUP BY [att].[Title]
END
