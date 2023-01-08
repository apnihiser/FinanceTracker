using FinanceTracker.Web.Models;

namespace FinanceTracker.Web.Utility
{
    public interface IRecordConsistency
    {
        Task MaintainTransactionConsistencyIfStatusChanged(TransactionUpdateViewModel input);
        Task MaintainTransactionConsistencyIfAccountChanged(TransactionUpdateViewModel input);
    }
}