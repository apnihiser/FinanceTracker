using FinanceTracker.Web.Models;

namespace FinanceTracker.Web.Utility
{
    public interface IRecordConsistency
    {
        Task MaintainTransactionConsistencyFromChanges(TransactionUpdateViewModel input);
        bool wasAccountChanged(int transactionId, int inputId);
    }
}