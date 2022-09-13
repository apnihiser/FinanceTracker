CREATE PROCEDURE [dbo].[spFullAccount_GetAll]
AS
SELECT [a].[Id], [a].[Title], [a].[Description], [a].[Type], [a].[Balance], [a].[HolderId], [p].[FirstName], [p].[LastName]
FROM dbo.Account a
INNER JOIN dbo.Payor p
ON a.HolderId = p.Id

