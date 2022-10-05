CREATE PROCEDURE [dbo].[spAccountCostByType_GetById]
	@ApplicationUserId int
AS
BEGIN
	SELECT SUM([att].[Balance]) as Sum, [att].[Type]
	FROM (
		SELECT *
		FROM [dbo].[Account]
		WHERE ApplicationUserId = @ApplicationUserId ) as att
	GROUP BY [att].[Type]
END
