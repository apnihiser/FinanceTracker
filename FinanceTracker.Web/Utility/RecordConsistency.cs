using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;
using Mailjet.Client.Resources.SMS;
using Microsoft.Identity.Client;

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

        // if both status and account changed in a transaction
        // we need to roll back the inbound transaction amount against the old transaction's account
        // we then need to add the new transaction amount to the new account
        public async Task MaintainTransactionConsistencyFromChanges(TransactionUpdateViewModel input)
        {
            var transaction = await GetExistingTransaction(input.Id);

            bool wasRecordCleared = WasInputRecordPreviouslyCleared(transaction.Status);

            bool isRecordNowCleared = IsInputRecordNowCleared(input.Status);

            await UpdateBalanceAndAccountIfChanged(wasRecordCleared, isRecordNowCleared, input, transaction);
        }

        public bool wasAccountChanged(int transactionId, int inputId)
        {
            string recordOldAccountId = transactionId.ToString();
            string recordNewInputId = inputId.ToString();

            return areAttributesTheSame(recordOldAccountId, recordNewInputId);
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

        private async Task UpdateBalanceAndAccountIfChanged(bool wasRecordCleared, bool isRecordNowCleared, TransactionUpdateViewModel input, TransactionModel transaction)
        {
            bool isSameAccount = wasAccountChanged(transaction.AccountId, input.AccountId);

            if ((wasRecordCleared != isRecordNowCleared) &&
                 (input.Status == cleared) &&
                 (isSameAccount == true))
            {
                await AddAmountToAccount(input.AccountId, input.AmountDue);
            }
            else if ((wasRecordCleared != isRecordNowCleared) &&
                      (input.Status == cleared) &&
                      (isSameAccount == false))
            {
                await UpdateAmountInNewAccount(input.AccountId, input.AmountDue);
            }
            else if ((wasRecordCleared != isRecordNowCleared) &&
                      (input.Status != cleared))
            {
                await RemoveAmountFromAccount(transaction.AccountId, transaction.Amount);
            }
            else if ((transaction.Status == cleared) &&
                      (input.Status == cleared) &&
                      (isSameAccount == true))
            {
                await UpdateAmountInAccount(input.AccountId, transaction.Amount, input.AmountDue);
            }
            else if ((transaction.Status == cleared) &&
                      (input.Status == cleared) &&
                      (isSameAccount == false))
            {
                await UpdateAmountsAndAccounts(transaction.AccountId, input.AccountId, transaction.Amount, input.AmountDue);
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

        private async Task UpdateAmountInNewAccount(int newAccountId, decimal newAmount)
        {
            // the old account was NON CLEARED so no activity was incurred,
            // HOWEVER
            // In the new account the new activity needs to be changed.
            // add input amount to new account
            var newAccount = await _accountData.GetAccountByAccountId(newAccountId);
            newAccount.Balance += newAmount;
            await _accountData.Update(newAccount);
        }

        private async Task UpdateAmountsAndAccounts(int oldAccountId, int newAccountId, decimal oldAmount, decimal newAmount)
        {
            // rollback input amount from old account
            var oldAccount = await _accountData.GetAccountByAccountId(oldAccountId);
            oldAccount.Balance -= oldAmount;
            await _accountData.Update(oldAccount);

            // add input amount to new account
            var newAccount = await _accountData.GetAccountByAccountId(newAccountId);
            newAccount.Balance += newAmount;
            await _accountData.Update(newAccount);
        }
    }
}
