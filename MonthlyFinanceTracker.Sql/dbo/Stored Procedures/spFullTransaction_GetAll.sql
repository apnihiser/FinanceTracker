CREATE PROCEDURE [dbo].[spFullTransaction_GetAll]
AS
BEGIN
	SELECT [t].[Id], [t].[AccountId], [t].[PayeeId], [t].[Amount], [t].[DueDate], [t].[Status], [pr].[Title] as ProviderName, [pr].[Service], [a].[Title] as AccountName
	FROM dbo.[Transaction] t
	INNER JOIN dbo.[Provider] pr
	ON t.PayeeId = pr.Id
	INNER JOIN dbo.[Payor] pa
	ON pr.PayorId = pa.Id
	INNER JOIN dbo.[Account] a
	ON pa.Id = a.HolderId
END
