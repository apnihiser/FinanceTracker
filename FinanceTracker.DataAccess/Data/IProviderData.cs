using FinanceTracker.DataAccess.Models;

namespace FinanceTracker.DataAccess.Data
{
    public interface IProviderData
    {
        Task<List<ProviderModel>> GetAllProviders();
        Task<ProviderModel?> GetProviderById(int id);
        Task<int> CreateProvider(ProviderModel model);
        Task DeleteProvider(int id);
        Task UpdateProvider(int id, string? service, string? title, string? url);
    }
}