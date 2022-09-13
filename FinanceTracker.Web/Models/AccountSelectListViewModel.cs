using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Models
{
    public class AccountSelectListViewModel
    {
        public string? Name { get; set; }

        public List<SelectListItem> Accounts { get; set; } = new List<SelectListItem>();
    }
}
