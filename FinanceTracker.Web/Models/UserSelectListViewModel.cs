using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Models
{
    public class UserSelectListViewModel
    {
        public string? Name { get; set; }

        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
    }
}
