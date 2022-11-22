using FinanceTracker.DataAccess.Models;

namespace FinanceTracker.DataAccess.Data
{
    public interface IProviderData
    {
        Task<List<ProviderModel>> GetAllProvidersByUserId(string userId);
        Task<ProviderModel?> GetProviderById(int id);
        Task<int> CreateProvider(int payorId, string title, string service, string url);
        Task DeleteProvider(int id);
        Task UpdateProvider(int id, string? service, string? title, string? url);
    }
}