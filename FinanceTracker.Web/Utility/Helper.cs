using FinanceTracker.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Utility
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string User = "User";

        public static List<SelectListItem> ConvertToSelectList(List<PayorModel> payors)
        {
            List<SelectListItem> selectList = new();

            payors.ForEach(x =>
            {
                selectList.Add(new SelectListItem
                {
                    Text = $"{x.FirstName} {x.LastName}",
                    Value = x.Id.ToString()
                });
            });

            return selectList;
        }
    }
}
