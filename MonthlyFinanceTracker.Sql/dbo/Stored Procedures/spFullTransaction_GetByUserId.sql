CREATE PROCEDURE [dbo].[spFullTransaction_GetByUserId]
	@ApplicationUserId int
AS
BEGIN
	SELECT [t].[Id], [t].[AccountId], [t].[PayeeId], [t].[Amount], [t].[DueDate], [t].[Status], [p].[Title] as ProviderName, [p].[Service], [a].[Title] as AccountName
	FROM [dbo].[Transaction] t
	INNER JOIN [dbo].[Account] a
	ON [t].[AccountId] = [a].[Id]
	INNER JOIN [dbo].[Provider] p
	ON [t].[PayeeId] = [p].[Id]
	WHERE [a].[ApplicationUserId] = @ApplicationUserId;
END
