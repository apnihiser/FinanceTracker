using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;

namespace FinanceTracker.Web.Utility
{
    public class RecordConsistency : IRecordConsistency
    {
        private readonly ITransactionData _transactionData;
        private readonly IAccountData _accountData;
        private readonly string cleared = "Cleared";

        public RecordConsistency(ITransactionData transactionData,
                                 IAccountData accountData)
        {
            _transactionData = transactionData;
            _accountData = accountData;
        }

        public async Task<TransactionUpdateViewModel> MaintainTransactionConsistencyIfStatusChanged(TransactionUpdateViewModel input)
        {
            var transaction = await GetExistingTransaction(input.Id);

            bool wasRecordCleared = WasInputRecordPreviouslyCleared(transaction.Status);

            bool isRecordNowCleared = IsInputRecordNowCleared(input.Status);

            await UpdateBalanceIfStatusChanged(wasRecordCleared, isRecordNowCleared, input, transaction);

            return input;
        }

        public async Task<TransactionUpdateViewModel> MaintainTransactionConsistencyIfAccountChanged(TransactionUpdateViewModel input)
        {
            var transaction = await GetExistingTransaction(input.Id);
            string recordOldAccountId = transaction.AccountId.ToString();
            string recordNewInputId = input.AccountId.ToString();

            bool areAccountsSame = areAttributesTheSame(recordOldAccountId, recordNewInputId);

            if (areAccountsSame == false)
            {
                await RemoveAmountFromAccount(transaction.AccountId, transaction.Amount);
                await AddAmountToAccount(input.AccountId, input.AmountDue);
            }

            return input;
        }

        private async Task<TransactionModel> GetExistingTransaction(int id)
        {
            var transaction = await _transactionData.GetFullTransactionById(id);

            return transaction;
        }

        private bool WasInputRecordPreviouslyCleared(string status)
        {
            return areAttributesTheSame(status, cleared);
        }

        private bool IsInputRecordNowCleared(string status)
        {
            return areAttributesTheSame(status, cleared);
        }

        private bool areAttributesTheSame(string input, string itemToCheckAgainst)
        {
            bool output = false;

            if (input == itemToCheckAgainst)
            {
                output = true;
            }

            return output;
        }

        private async Task UpdateBalanceIfStatusChanged(bool wasRecordCleared, bool isRecordNowCleared, TransactionUpdateViewModel input, TransactionModel transaction)
        {
            // Has the record Status been changed by the user to "Cleared"? Then add amount to Balance
            if (wasRecordCleared != isRecordNowCleared && input.Status == "Cleared")
            {
                await AddAmountToAccount(input.AccountId, input.AmountDue);
            }
            // Has the record Status been changed by the user to "Not Cleared"? Then Rollback previous "Cleared" amount added.
            else if (wasRecordCleared != isRecordNowCleared && input.Status != "Cleared")
            {
                await RemoveAmountFromAccount(input.AccountId, transaction.Amount);
            }
            // Has the Balance changed when the Transaction Status remained "Cleared? Then we need to remove the previous "Cleared" balance and Add new "Cleared" balance.
            else if (transaction.Status == "Cleared" && input.Status == "Cleared")
            {
                await UpdateAmountInAccount(input.AccountId, transaction.Amount, input.AmountDue);
            }
        }

        private async Task AddAmountToAccount(int accountId, decimal amount)
        {
            var account = await _accountData.GetAccountByAccountId(accountId);

            account.Balance += amount;

            await _accountData.Update(account);
        }

        private async Task RemoveAmountFromAccount(int accountId, decimal amount)
        {
            var account = await _accountData.GetAccountByAccountId(accountId);

            account.Balance -= amount;

            await _accountData.Update(account);
        }

        private async Task UpdateAmountInAccount(int accountId, decimal previousAmount, decimal newAmount)
        {
            var account = await _accountData.GetAccountByAccountId(accountId);

            account.Balance -= previousAmount;

            account.Balance += newAmount;

            await _accountData.Update(account);
        }
    }
}
