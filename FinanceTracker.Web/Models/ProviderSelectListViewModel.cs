using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Models
{
    public class ProviderSelectListViewModel
    {
        public string? Name { get; set; }

        public List<SelectListItem> Providers { get; set; } = new List<SelectListItem>();
    }
}
