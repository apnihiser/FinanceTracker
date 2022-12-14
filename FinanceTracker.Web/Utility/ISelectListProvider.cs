using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Utility
{
    public interface ISelectListProvider
    {
        Task<List<SelectListItem>> AccountSelectList();
        Task<List<SelectListItem>> ProviderSelectList();
        List<SelectListItem> StatusSelectList();
        List<SelectListItem> TransactionTypeSelectList();
    }
}