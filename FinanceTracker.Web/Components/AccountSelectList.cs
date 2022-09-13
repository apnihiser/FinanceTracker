using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using FinanceTracker.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Components
{
    public class AccountSelectList : ViewComponent
    {
        private readonly IAccountData _accountData;

        public AccountSelectList(IAccountData accountData)
        {
            _accountData = accountData;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<AccountModel> accounts = await _accountData.GetAllAccounts();

            if (accounts is null)
            {
                return View();
            }

            AccountSelectListViewModel selectList = new();

            accounts.ForEach( x =>
            {
                selectList.Accounts.Add( new SelectListItem { Text = x.Title, Value = x.Id.ToString() } );
            });

            return View(selectList);
        }
    }
}
