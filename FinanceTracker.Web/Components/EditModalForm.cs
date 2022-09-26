using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinanceTracker.Web.Components
{
    public class EditModalForm : ViewComponent
    {
        public IViewComponentResult Invoke(TransactionViewModel input)
        {
            return View(input);
        }
    }
}
