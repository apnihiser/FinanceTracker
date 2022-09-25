using FinanceTracker.DataAccess.Models;

namespace FinanceTracker.DataAccess.Data
{
    public interface ITransactionData
    {
        Task<TransactionModel> GetFullTransactionById(int Id);
        Task<List<TransactionModel>> GetAllFullTransactions();
        Task<List<TransactionModel>> GetAllFullTransactionsByUserIdAsync(string id);
        Task DeleteTransactionById(int id);
        Task EditTransactionById(TransactionModel record);
        Task CreateTransaction(TransactionModel input);
        Task<List<TransactionModel>> GetAllFullTransactionsByUserAndMonthIdAsync(string id, DateTime dateTime);
    }
}