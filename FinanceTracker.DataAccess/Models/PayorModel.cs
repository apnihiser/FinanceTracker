using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.DataAccess.Models
{
    public class PayorModel
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
