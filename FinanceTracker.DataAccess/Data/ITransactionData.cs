using FinanceTracker.DataAccess.Models;

namespace FinanceTracker.DataAccess.Data
{
    public interface ITransactionData
    {
        Task<int> CreateTransaction(TransactionModel input);
        Task DeleteTransactionById(int id);
        Task<List<TransactionModel>> GetFullTransactionsByProviderId(int providerId);
        Task DeleteTransactionByAccountId(int accountId);
        Task DeleteTransactionByProviderId(int providerId);
        Task EditTransactionById(TransactionModel record);
        Task<List<TransactionModel>> GetAllFullTransactions();
        Task<List<TransactionModel>> GetAllFullTransactionsByUserIdAsync(string id);
        Task<TransactionModel> GetFullTransactionById(int id);
        Task<List<TransactionProviderAmount>> GetTransactionProviderChartDataByMonth(string id, DateTime dateTime, string transactionType);
        Task<List<TransactionStatusAmount>> GetTransactionStatusChartDataByMonth(string id, DateTime dateTime);
        Task<List<TransactionStatusCount>> GetTransactionStatusCountChartDataByMonth(string id, DateTime dateTime);
        Task<List<TransactionModel>> GetUserTransactionsByMonth(string id, DateTime dateTime);
    }
}