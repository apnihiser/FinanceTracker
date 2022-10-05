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
        Task<List<TransactionModel>> GetUserTransactionsByMonth(string id, DateTime dateTime);
        Task<List<TransactionProviderAmount>> GetTransactionProviderChartDataByMonth(string id, DateTime dateTime);
        Task<List<TransactionStatusAmount>> GetTransactionStatusChartDataByMonth(string id, DateTime dateTime);
        Task<List<TransactionStatusCount>> GetTransactionStatusCountChartDataByMonth(string id, DateTime dateTime);
    }
}