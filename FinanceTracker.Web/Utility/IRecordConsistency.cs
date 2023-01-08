using FinanceTracker.Web.Models;

namespace FinanceTracker.Web.Utility
{
    public interface IRecordConsistency
    {
        Task<TransactionUpdateViewModel> MaintainTransactionConsistencyIfStatusChanged(TransactionUpdateViewModel input);
        Task<TransactionUpdateViewModel> MaintainTransactionConsistencyIfAccountChanged(TransactionUpdateViewModel input);
    }
}