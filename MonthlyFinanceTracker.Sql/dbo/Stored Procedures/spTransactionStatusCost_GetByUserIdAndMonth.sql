CREATE PROCEDURE [dbo].[spTransactionStatusCost_GetByUserIdAndMonth]
	@ApplicationUserId int,
	@TargetDate DateTime2
AS
BEGIN
	SELECT Sum([tst].[Amount]) as Amount, [tst].[Status]
	FROM 
		(SELECT [t].[Status], [t].[Amount]
			FROM [dbo].[Transaction] t
			INNER JOIN [dbo].[Account] a
			ON [t].[AccountId] = [a].[Id]
			INNER JOIN [dbo].[Provider] p
			ON [t].[PayeeId] = [p].[Id]
			WHERE  [a].[ApplicationUserId] = @ApplicationUserId and
				[t].[DueDate] >= DATEFROMPARTS(YEAR(@TargetDate), MONTH(@TargetDate), 1) AND
				[t].[DueDate] <  DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(@TargetDate), MONTH(@TargetDate), 1))) as tst
	GROUP BY [tst].[Status];
END
