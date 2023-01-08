using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace FinanceTracker.Web.Utility
{
    public class SelectListProvider : ISelectListProvider
    {
        private readonly IAccountData _accountData;
        private readonly IProviderData _providerData;
        private readonly IHttpContextAccessor _contextAccessor;
        private static readonly string _due = "Due";
        private static readonly string _cleared = "Cleared";
        private static readonly string _late = "Late";
        private static readonly string _deposit = "Deposit";
        private static readonly string _withdrawal = "Withdrawl";
        private readonly string _userId;

        public SelectListProvider(IAccountData accountData, IProviderData providerData, IHttpContextAccessor contextAccessor)
        {
            _accountData = accountData;
            _providerData = providerData;
            _contextAccessor = contextAccessor;
            _userId = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public async Task<List<SelectListItem>> ProviderSelectList()
        {
            List<ProviderModel> providers = await _providerData.GetAllProvidersByUserId(_userId);

            if (providers is null)
            {
                return null;
            }

            List<SelectListItem> output = new();

            providers.ForEach(x =>
            {
                output.Add(new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            });

            return output;
        }

        public async Task<List<SelectListItem>> AccountSelectList()
        {
            List<AccountModel> providers = await _accountData.GetAccountsByUserId(_userId);

            if (providers is null)
            {
                return null;
            }

            List<SelectListItem> output = new();

            providers.ForEach(x =>
            {
                output.Add(new SelectListItem { Text = x.Title, Value = x.Id.ToString() });
            });

            return output;
        }

        public List<SelectListItem> StatusSelectList()
        {
            List<SelectListItem> output = new();

            output.Add(new SelectListItem { Text = _due, Value = _due });
            output.Add(new SelectListItem { Text = _cleared, Value = _cleared });
            output.Add(new SelectListItem { Text = _late, Value = _late });

            return output;
        }

        public List<SelectListItem> TransactionTypeSelectList()
        {
            List<SelectListItem> output = new();

            output.Add(new SelectListItem { Text = _deposit, Value = _deposit });
            output.Add(new SelectListItem { Text = _withdrawal, Value = _withdrawal });

            return output;
        }
    }
}
