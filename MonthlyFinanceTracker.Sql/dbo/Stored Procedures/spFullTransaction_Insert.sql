CREATE PROCEDURE [dbo].[spFullTransaction_Insert]
	--@TransactionType TransactionType READONLY Move from datatype to dynamic parameter
	@AccountId int,
	@PayeeId int,
	@TransactionReason NVARCHAR(20),
	@Type NVARCHAR(10),
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
		,[TransactionReason]
		,[Type]
		,[Amount]
		,[DueDate]
		,[Status])
	VALUES
		(@AccountId
		,@PayeeId
		,@TransactionReason
		,@Type
		,@Amount
		,@DueDate
		,@Status)

	set @Id = SCOPE_IDENTITY();
END
