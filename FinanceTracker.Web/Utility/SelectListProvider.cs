﻿using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Utility
{
    public class SelectListProvider : ISelectListProvider
    {
        private readonly IAccountData _accountData;
        private readonly IProviderData _providerData;
        private static readonly string _due = "Due";
        private static readonly string _scheduled = "Scheduled";
        private static readonly string _cleared = "Cleared";
        private static readonly string _late = "Late";

        public SelectListProvider(IAccountData accountData, IProviderData providerData)
        {
            _accountData = accountData;
            _providerData = providerData;
        }

        public async Task<List<SelectListItem>> ProviderSelectList()
        {
            List<ProviderModel> providers = await _providerData.GetAllProviders();

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
            List<AccountModel> providers = await _accountData.GetAllAccounts();

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
            output.Add(new SelectListItem { Text = _scheduled, Value = _scheduled });
            output.Add(new SelectListItem { Text = _cleared, Value = _cleared });
            output.Add(new SelectListItem { Text = _late, Value = _late });

            return output;
        }
    }
}