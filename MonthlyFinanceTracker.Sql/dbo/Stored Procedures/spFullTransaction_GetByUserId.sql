CREATE PROCEDURE [dbo].[spFullTransaction_GetByUserId]
	@ApplicationUserId int
AS
BEGIN
	SELECT [t].[Id], [t].[AccountId], [t].[PayeeId], [t].[Amount], [t].[DueDate], [t].[Status], [pr].[Title] as ProviderName, [pr].[Service], [a].[Title] as AccountName
	FROM dbo.[Transaction] t
	INNER JOIN dbo.[Provider] pr
	ON t.PayeeId = pr.Id
	INNER JOIN dbo.[ApplicationUser] pa
	ON pr.UserId = pa.ApplicationUserId
	INNER JOIN dbo.[Account] a
	ON pa.ApplicationUserId = a.ApplicationUserId
	WHERE pa.ApplicationUserId = @ApplicationUserId
END
