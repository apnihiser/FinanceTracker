using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Components
{
    public class ProviderSelectList : ViewComponent
    {
        private readonly IProviderData _providerData;

        public ProviderSelectList(IProviderData providerData)
        {
            _providerData = providerData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<ProviderModel> providers = await _providerData.GetAllProviders();

            if (providers is null)
            {
                return View();
            }

            ProviderSelectListViewModel selectList = new();

            providers.ForEach( x =>
            {
                selectList.Providers.Add( new SelectListItem { Text = x.Title, Value = x.Id.ToString() } );
            });

            return View(selectList);
        }
    }
}
