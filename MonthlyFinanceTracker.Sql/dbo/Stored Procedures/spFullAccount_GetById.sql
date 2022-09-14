CREATE PROCEDURE [dbo].[spFullAccount_GetById]
	@Id int
AS
SELECT [a].[Id], [a].[Title], [a].[Description], [a].[Type], [a].[Balance], [a].[ApplicationUserId], [ap].[Fullname]
FROM dbo.Account a
INNER JOIN dbo.ApplicationUser ap
ON a.ApplicationUserId = ap.ApplicationUserId
WHERE [a].[Id] = @Id
