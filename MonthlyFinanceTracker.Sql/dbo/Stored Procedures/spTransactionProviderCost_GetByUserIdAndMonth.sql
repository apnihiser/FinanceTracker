CREATE PROCEDURE [dbo].[spTransactionProviderCost_GetByUserIdAndMonth]
	@ApplicationUserId int,
	@TargetDate DateTime2
AS
BEGIN
	SELECT SUM([tpt].[Amount]) as Amount, [tpt].[Title] as ProviderName
	FROM
		(SELECT [t].[Amount], [p].[Title]
		FROM [dbo].[Transaction] t
		INNER JOIN [dbo].[Account] a
		ON [t].[AccountId] = [a].[Id]
		INNER JOIN [dbo].[Provider] p
		ON [t].[PayeeId] = [p].[Id]
		WHERE [a].[ApplicationUserId] = @ApplicationUserId and
			  [t].[DueDate] >= DATEFROMPARTS(YEAR(@TargetDate), MONTH(@TargetDate), 1) AND
			  [t].[DueDate] <  DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(@TargetDate), MONTH(@TargetDate), 1))) as tpt
	GROUP BY Title;
END
