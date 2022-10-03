DECLARE @ApplicationUserId INT = 1;
DECLARE @TargetDate DATETIME2 = '09/01/2022';
	
SELECT COUNT([t].[Status]) as Count, [t].[Status]
FROM [dbo].[Transaction] t
INNER JOIN [dbo].[Account] a
ON [t].[AccountId] = [a].[Id]
INNER JOIN [dbo].[Provider] p
ON [t].[PayeeId] = [p].[Id]
GROUP BY [a].[ApplicationUserId], [t].[DueDate], [t].[Status]
HAVING  [a].[ApplicationUserId] = @ApplicationUserId and
		[t].[DueDate] >= DATEFROMPARTS(YEAR(@TargetDate), MONTH(@TargetDate), 1) AND
		[t].[DueDate] <  DATEADD(MONTH, 1, DATEFROMPARTS(YEAR(@TargetDate), MONTH(@TargetDate), 1));

SELECT *
FROM [dbo].[Transaction]