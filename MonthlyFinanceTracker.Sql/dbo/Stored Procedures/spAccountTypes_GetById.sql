CREATE PROCEDURE [dbo].[spAccountTypes_GetById]
	@ApplicationUserId INT
AS
BEGIN
	SELECT COUNT([att].[Type]) as Count, [att].[Type] as Name
	FROM 
		(SELECT *
		 FROM [dbo].[Account]
		 WHERE ApplicationUserId = @ApplicationUserId) as att
	GROUP BY [att].[Type];
END
