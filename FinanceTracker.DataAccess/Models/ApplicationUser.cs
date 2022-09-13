using Microsoft.AspNetCore.Identity;

namespace FinanceTracker.DataAccess.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }

}
