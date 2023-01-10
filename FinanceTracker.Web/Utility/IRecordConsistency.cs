using FinanceTracker.Web.Enums;
using FinanceTracker.Web.Models;

namespace FinanceTracker.Web.Utility
{
    public interface IAccountBalanceService
    {
        Task UpdateActionAccountBalance(TransactionUpdateViewModel input);
        Task CreateActionAccountBalance(TransactionUpdateViewModel input);
        Task DeleteActionAccountBalance(TransactionUpdateViewModel input);
        //bool wasAccountChanged(int transactionId, int inputId);
    }
}