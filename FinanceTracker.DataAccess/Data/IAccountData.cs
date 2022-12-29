using FinanceTracker.DataAccess.Models;

namespace FinanceTracker.DataAccess.Data
{
    public interface IAccountData
    {
        Task<int> Create(string? title, string? description, string? type, decimal balance, int holderId);
        Task Delete(int id);
        Task<List<AccountTypeCount>> GetAccountCountByUserId(string id);
        Task<List<AccountProviderCost>> GetAccountProviderCostByUserId(string id);
        Task<AccountModel?> GetAccountByUserId(int id);
        Task<List<AccountModel>> GetAccountsByUserId(string userId);
        Task<List<AccountTypeCost>> GetAccountTypeCostByUserId(string id);
        Task<List<AccountModel>> GetAllAccounts();
        Task<List<FullAccountModel>> GetAllFullAccountsByUserId(string userId);
        Task<FullAccountModel> GetFullAccountByHolderId(int id);
        Task Update(AccountModel accountRecord);
        Task<AccountModel> GetAccountByAccountId(int id);
    }
}