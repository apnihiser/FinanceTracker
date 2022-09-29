using FinanceTracker.DataAccess.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinanceTracker.Web.Utility
{
    public static class Helper
    {
        public static string Admin = "Admin";
        public static string User = "User";
        public static int success_code = 1;
        public static int failure_code = 0;
        public static string ChartLoadSuccessful = "Transactions Loaded Successfully...";
    }
}
