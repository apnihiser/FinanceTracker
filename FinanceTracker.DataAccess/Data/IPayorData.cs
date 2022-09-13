using FinanceTracker.DataAccess.Models;

namespace FinanceTracker.DataAccess.Data
{
    public interface IPayorData
    {
        Task<List<PayorModel>> GetAllPayors();
        Task<PayorModel?> GetPayorById(int id);
        Task<int> CreatePayor(string? firstName, string? lastName);
        Task DeletePayor(int id);
        Task UpdatePayor(int id, string? firstName, string? lastName);
    }
}