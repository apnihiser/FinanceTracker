using FinanceTracker.DataAccess.Models;

namespace FinanceTracker.DataAccess.Data
{
    public interface IAccountData
    {
        Task<int> Create(string? title, string? description, string? type, decimal balance, int holderId);
        Task Delete(int id);
        Task<AccountModel?> GetAccountsByUserId(int id);
        Task<List<AccountModel>> GetAllAccounts();
        Task Update(AccountModel accountRecord);
        Task<List<FullAccountModel>> GetAllFullAccounts();
        Task<FullAccountModel> GetFullAccountByHolderId(int id);
    }
}