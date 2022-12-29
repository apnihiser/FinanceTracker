CREATE PROCEDURE [dbo].[spFullTransaction_Insert]
	--@TransactionType TransactionType READONLY Move from datatype to dynamic parameter
	@AccountId int,
	@PayeeId int,
	@Amount Money,
	@DueDate DateTime2,
	@Status NVARCHAR(50),
	@Id int output
AS
BEGIN
	SET NOCOUNT ON;

	INSERT INTO [dbo].[Transaction]
		([AccountId]
		,[PayeeId]
		,[Amount]
		,[DueDate]
		,[Status])
	VALUES
		(@AccountId
		,@PayeeId
		,@Amount
		,@DueDate
		,@Status)

	set @Id = SCOPE_IDENTITY();
END
