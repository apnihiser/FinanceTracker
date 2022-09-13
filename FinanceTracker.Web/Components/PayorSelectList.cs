using FinanceTracker.DataAccess.Data;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Components
{
    public class PayorSelectListViewComponent : ViewComponent
    {
        private readonly IPayorData _payorData;

        public PayorSelectListViewComponent(IPayorData payorData)
        {
            _payorData = payorData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var rows = await _payorData.GetAllPayors();

            var output = new UserSelectListViewModel();

            foreach (var row in rows)
            {
                output.Users.Add(new SelectListItem
                {
                    Value = row.Id.ToString(),
                    Text = $"{row.FirstName} {row.LastName}"
                });
            }

            return View(output);
        }
    }
}
