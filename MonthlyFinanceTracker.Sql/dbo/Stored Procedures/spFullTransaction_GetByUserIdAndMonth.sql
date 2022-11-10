CREATE PROCEDURE [dbo].[spFullTransaction_GetByUserIdAndMonth]
	@ApplicationUserId int,
	@TargetDate DateTime2
AS
BEGIN
	SELECT [t].[Id], [t].[AccountId], [t].[PayeeId], [t].[Amount], [t].[DueDate], [t].[Status], [p].[Title] as ProviderName, [p].[Service], [a].[Title] as AccountName
	FROM [dbo].[Transaction] t
	INNER JOIN [dbo].[Account] a
	ON [t].[AccountId] = [a].[Id]
	INNER JOIN [dbo].[Provider] p
	ON [t].[PayeeId] = [p].[Id]
	WHERE [a].[ApplicationUserId] = @ApplicationUserId and
			[t].[DueDate] >= DATEFROMPARTS(YEAR(@TargetDate), MONTH(@TargetDate), 1) AND
			[t].[DueDate] <  DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(@TargetDate), MONTH(@TargetDate), 1))
END
