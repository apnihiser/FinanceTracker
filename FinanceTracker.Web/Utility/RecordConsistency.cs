using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Enums;
using FinanceTracker.Web.Models;
using Mailjet.Client.Resources.SMS;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.Web.Utility
{
    public class RecordConsistency : IAccountBalanceService
    {
        private readonly ITransactionData _transactionData;
        private readonly IAccountData _accountData;
        private readonly string cleared = "Cleared";
        private readonly string deposit = "Deposit";
        private readonly string withdrawal = "Withdrawal";

        public RecordConsistency(ITransactionData transactionData,
                                 IAccountData accountData)
        {
            _transactionData = transactionData;
            _accountData = accountData;
        }

        public async Task UpdateActionAccountBalance(TransactionUpdateViewModel input)
        {
            var transaction = await GetExistingTransaction(input.Id);

            bool wasCleared = WasTransactionCleared(transaction.Status);

            bool isCleared = IsInputCleared(input.Status);

            bool wasDeposit = WasTransactionDeposit(transaction.Type);

            bool isDeposit = IsInputDeposit(input.Type);

            await UpdateAccountIfChanged(wasDeposit, isDeposit, wasCleared, isCleared, input, transaction);
        }

        public async Task CreateActionAccountBalance(TransactionUpdateViewModel input)
        {
            if (input.Status == cleared && input.Type == deposit)
            {
                await AccountDeposit(input.AccountId, input.AmountDue);
            }
            else if (input.Status == cleared && input.Type == withdrawal)
            {
                await AccountWithdrawal(input.AccountId, input.AmountDue);
            }
        }

        public async Task DeleteActionAccountBalance(TransactionUpdateViewModel input, bool requestfromProvider = false)
        {
            if(requestfromProvider == false)
            {
                var transaction = await GetExistingTransaction(input.Id);

                if (input.Status == cleared && input.Type == deposit)
                {
                    await AccountDepositRollback(transaction.AccountId, transaction.Amount);
                }
                else if (input.Status == cleared && input.Type == withdrawal)
                {
                    await AccountWithdrawalRollback(transaction.AccountId, transaction.Amount);
                }
            }
            else
            {
                if (input.Status == cleared && input.Type == deposit)
                {
                    await AccountDepositRollback(input.AccountId, input.AmountDue);
                }
                else if (input.Status == cleared && input.Type == withdrawal)
                {
                    await AccountWithdrawalRollback(input.AccountId, input.AmountDue);
                }
            }

        }

        private async Task<TransactionModel> GetExistingTransaction(int id)
        {
            var transaction = await _transactionData.GetFullTransactionById(id);

            return transaction;
        }

        private bool WasTransactionCleared(string status)
        {
            return AreAttributesTheSame(status, cleared);
        }

        private bool IsInputCleared(string status)
        {
            return AreAttributesTheSame(status, cleared);
        }

        private bool WasTransactionDeposit(string type)
        {
            return AreAttributesTheSame(type, deposit);
        }

        private bool IsInputDeposit(string type)
        {
            return AreAttributesTheSame(type, deposit);
        }

        private bool AreAttributesTheSame(string input, string itemToCheckAgainst)
        {
            bool output = false;

            if (input == itemToCheckAgainst)
            {
                output = true;
            }

            return output;
        }

        private async Task UpdateAccountIfChanged(bool wasDeposit, bool isDeposit, bool wasCleared, bool isCleared, TransactionUpdateViewModel input, TransactionModel transaction)
        {
            bool hasStatusChangedToCleared = (wasCleared != isCleared) && (input.Status == cleared);
            bool hasStatusChangedtoNotCleared = (wasCleared != isCleared) && (input.Status != cleared);
            bool hasStatusRemainedCleared = (wasCleared == isCleared) && (input.Status == cleared);
            bool hasTypeChangedToDeposit = (wasDeposit != isDeposit) && (input.Type == deposit);
            bool hasTypeChangedtoWithdrawal = (transaction.Type != input.Type) && (input.Type == withdrawal);
            bool hasTypeRemainedDeposit = (wasDeposit == isDeposit) && (input.Type == deposit);
            bool hasTypeRemainedWithdrawal = (transaction.Type == input.Type) && (input.Type == withdrawal);

            /* ----- LOGIC TREE TO CONSIDER -----
             1. If the Record was notChangedToCleared, then there are no inccured changes to any accounts.
                
             2. Did the user input change the old transaction record to "Cleared"? If so, that means the related account needs to be updated to the new input value. 
                 a. If the account stayed the same then update the value.
                         1. If the type is DEPOSIT then add the value. Also did the type change?
                         2. if the type is withdrawal then subtract the value. Also did the type change?
                 b. If the account changed then the input balance will be updated on the new account that the user selected. No affect to the old account, since it was "Not Cleared"
                         1. If the type is DEPOSIT then add the value. Also did the type change?
                         2. if the type is withdrawal then subtract the value. Also did the type change?
                 NOTE: We really don't even care if the account changes as we will ONLY apply changes to the account located on the input request. This is because there is no incurred activity
                       on the old account
                 NOTE: Doesn't matter if TYPE changed here either. If it changed during update then the new input request will update accordingly.
             
             3. Did the user input change the old transaction record to "NotCleared?" If so, then roll back the previously "Cleared Charge" on the account
                 a. If the account stayed the same then the value will need to be rolled back on this account.
                         1. If the type was a DEPOSIT then subtract the value. ROLLBACK DEPOSIT
                         2. if the type is WITHDRAWAL then add the value. ROLLBACK WITHDRAWAL
                 b. If the account changed then the value will need to be updated on the OLD account! Nothing will be updated on the new account since it is now "Not Cleared".
                         1. If the type was a DEPOSIT then subtract the value. ROLLBACK DEPOSIT
                         2. If the type is WITHDRAWAL then add the value. ROLLBACK WITHDRAWAL
                 NOTE: We don't care about the account change here either, because we will only rollback changes to the account located on the old transaction record. This is because no 
                       activity will be performed on the account in the input, because it is "Not Cleared"
                 NOTE: Doesn't matter if TYPE changed here either. If it changed, the new input request will not cause any activity, and we will already be checking against the old transaction 
                       type and act accordingly.
               
             4. Did the user input NOT make any changes to a previously "Cleared" transaction? If so, then any other changes will need to be made to accounts.
                 a. If the account stayed the same then the old value will need to rolled back and the new value will need to be updated on this account.
                         1. If the input request is a DEPOSIT and a change didn't occur then the DEPOSIT would need to be rolled back and added to the account
                            a. If a type change did occur to be a DEPOSIT the the transaction would be a WITHDRAWAL from the account, 
                               which would need to be rolled back, and the DEPOSIT updated to the account.
                         2. If the input request is a WITHDRAWAL and a change didn't occur then the WITHDRAWAL would need to be rolled back, and added to the account
                            b. If a type change did occur to be a WITHDRAWAL the the transaction would be a DEPOSIT from the account, 
                               which would need to be rolled back, and the WITHDRAWAL updated to the account.
                 b. If the account was changed, then the value will also need to be rolled back on the previous account, and updated on the new account
                         1. If the input request is a DEPOSIT and a change did occur then the DEPOSIT would need to be rolled back on the old account and added to the new account
                            a. If a type change did occur to be a DEPOSIT the the transaction would be a WITHDRAWAL from the old account, 
                               which would need to be rolled back, and the DEPOSIT updated to the new account.
                         2. If the input request is a WITHDRAWAL and a change didn't occur then the WITHDRAWAL would need to be rolled back from the old account, and added to the new account
                            b. If a type change did occur to be a WITHDRAWAL the the transaction would be a DEPOSIT from the old account, 
                               which would need to be rolled back, and the WITHDRAWAL updated to the new account. 
                 NOTE: Since we are always performing ROLLBACKS against previous transactions, and UPDATING new activity on the INPUT request accounts, then we really don't care about
                       If Account has changed. We know that the Status has remained "Cleared" in this scenario so we only will NEED to rollback the old transaction related account and 
                       update the new INPUT related account. So if the account changed or not is irrelent for this operation.
                 NOTE: It is important to understand that there was a change to a new type. This informs us that the old type will need to be rolled back appropriately. And the new
                       Type of transaction to post to the account will be from the new input request.
            */

            // ----- Tree 2
            if (hasStatusChangedToCleared && isDeposit)
            {
                await AccountDeposit(input.AccountId, input.AmountDue);
            }
            else if(hasStatusChangedToCleared && isDeposit == false)
            {
                await AccountWithdrawal(input.AccountId, input.AmountDue);
            }

            // ----- Tree 3
            if (hasStatusChangedtoNotCleared && isDeposit)
            {
                await AccountDepositRollback(transaction.AccountId, transaction.Amount);
            }
            else if(hasStatusChangedtoNotCleared && isDeposit == false)
            {
                await AccountWithdrawalRollback(transaction.AccountId, transaction.Amount);
            }

            // ----- tree 4
            if (hasStatusRemainedCleared && hasTypeRemainedDeposit)
            {
                await AccountDepositRollback(transaction.AccountId, transaction.Amount);
                await AccountDeposit(input.AccountId, input.AmountDue);
            }
            else if (hasStatusRemainedCleared && hasTypeChangedToDeposit)
            {
                await AccountWithdrawalRollback(transaction.AccountId, transaction.Amount);
                await AccountDeposit(input.AccountId, input.AmountDue);
            }
            else if (hasStatusRemainedCleared && hasTypeRemainedWithdrawal)
            {
                await AccountWithdrawalRollback(transaction.AccountId, transaction.Amount);
                await AccountWithdrawal(input.AccountId, input.AmountDue);
            }
            else if(hasStatusRemainedCleared && hasTypeChangedtoWithdrawal)
            {
                await AccountDepositRollback(transaction.AccountId, transaction.Amount);
                await AccountWithdrawal(input.AccountId, input.AmountDue);
            }
        }

        private async Task AccountDeposit(int accountId, decimal amount)
        {
            var account = await _accountData.GetAccountByAccountId(accountId);

            account.Balance += amount;

            await _accountData.Update(account);
        }

        private async Task AccountWithdrawal(int accountId, decimal amount)
        {
            var account = await _accountData.GetAccountByAccountId(accountId);

            account.Balance -= amount;

            await _accountData.Update(account);
        }

        private async Task AccountDepositRollback(int accountId, decimal previousValue)
        {
            var account = await _accountData.GetAccountByAccountId(accountId);
            account.Balance -= previousValue;
            await _accountData.Update(account);
        }

        private async Task AccountWithdrawalRollback(int accountId, decimal previousValue)
        {
            var account = await _accountData.GetAccountByAccountId(accountId);
            account.Balance += previousValue;
            await _accountData.Update(account);
        }
    }
}
