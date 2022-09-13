using FinanceTracker.DataAccess.Models;

namespace FinanceTracker.DataAccess.Data
{
    public interface ITransactionData
    {
        Task<List<TransactionModel>> GetAllFullTransactions();
    }
}