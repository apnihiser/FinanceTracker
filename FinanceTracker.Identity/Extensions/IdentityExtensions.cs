using FinanceTracker.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Identity.Extensions
{
    public static class IdentityExtensions
    {
        public static Task SetFullNameAsync(this IUserStore<ApplicationUserIdentity> userStore, ApplicationUserIdentity user, string userFullName, CancellationToken cancellationToken )
        {
            user.Fullname = userFullName;
            return Task.FromResult(0);
        }
    }
}
