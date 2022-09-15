using Dapper;
using FinanceTracker.DataAccess.Data;
using FinanceTracker.DataAccess.Database;
using FinanceTracker.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.Identity
{
    public class UserStore : IUserStore<ApplicationUserIdentity>, 
                             IUserEmailStore<ApplicationUserIdentity>, 
                             IUserPhoneNumberStore<ApplicationUserIdentity>, 
                             IUserTwoFactorStore<ApplicationUserIdentity>, 
                             IUserPasswordStore<ApplicationUserIdentity>
                             
    {
        private readonly IDatabaseAccess _db;
        private readonly ConnectionStringData _connectionString;

        public UserStore(IDatabaseAccess db, ConnectionStringData connectionString)
        {
            _db = db;
            _connectionString = connectionString;
        }

        // userstore methods
        public async Task<IdentityResult> CreateAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            DataTable dataTable = new();

            dataTable.Columns.Add("Fullname", typeof(string));
            dataTable.Columns.Add("Username", typeof(string));
            dataTable.Columns.Add("NormalizedUsername", typeof(string));
            dataTable.Columns.Add("Email", typeof(string));
            dataTable.Columns.Add("NormalizedEmail", typeof(string));
            dataTable.Columns.Add("EmailConfirmed", typeof(bool));
            dataTable.Columns.Add("PasswordHash", typeof(string));
            dataTable.Columns.Add("PhoneNumber", typeof(string));
            dataTable.Columns.Add("PhoneNumberConfirmed", typeof(bool));
            dataTable.Columns.Add("TwoFactorEnabled", typeof(bool));

            dataTable.Rows.Add(
                user.Fullname,
                user.Username,
                user.NormalizedUsername,
                user.Email,
                user.NormalizedEmail,
                user.EmailConfirmed,
                user.PasswordHash,
                user.PhoneNumber,
                user.PhoneNumberConfirmed,
                user.TwoFactorEnabled);

            await _db.SaveData("dbo.spApplicationUser_Insert", new { Account = dataTable.AsTableValuedParameter("dbo.ApplicationUser") }, _connectionString.Name, cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            await _db.SaveData("dbo.spApplicationUser_DeleteById", user, _connectionString.Name, cancellationToken);

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            // nothing to dispose
        }

        public async Task<ApplicationUserIdentity> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var output = await _db.LoadData<ApplicationUserIdentity, dynamic>("dbo.spApplicationUser_GetById", new { Id = userId }, _connectionString.Name, cancellationToken);

            return output.First();
        }

        public async Task<ApplicationUserIdentity> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var output = await _db.LoadData<ApplicationUserIdentity, dynamic>("dbo.spApplicationUser_FindByName", new { NormalizedUsername = normalizedUserName }, _connectionString.Name, cancellationToken);

            return output.First();
        }

        public Task<string> GetNormalizedUserNameAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedUsername);
        }

        public Task<string> GetUserIdAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.ApplicationUserId.ToString());
        }

        public Task<string> GetUserNameAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Username);
        }

        public Task SetNormalizedUserNameAsync(ApplicationUserIdentity user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUsername = normalizedName;
            return Task.FromResult(0);
        }

        public Task SetUserNameAsync(ApplicationUserIdentity user, string userName, CancellationToken cancellationToken)
        {
            user.Username = userName;
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            await _db.SaveData(
                "dbo.spApplicationUser_UpdateById", 
                new 
                {
                    ApplicationUserId = user.ApplicationUserId,
                    Fullname = user.Fullname,
                    Username = user.Username,
                    NormalizedUsername = user.NormalizedUsername,
                    Email = user.Email,
                    NormalizedEmail = user.NormalizedEmail,
                    EmailConfirmed = user.EmailConfirmed,
                    PasswordHash = user.PasswordHash,
                    PhoneNumber = user.PhoneNumber,
                    PhoneNumberConfirmed = user.PhoneNumberConfirmed,
                    TwoFactorEnabled = user.TwoFactorEnabled,
                },
                _connectionString.Name,
                cancellationToken);

            return IdentityResult.Success;
        }


        // emailstore methods
        public Task SetEmailAsync(ApplicationUserIdentity user, string email, CancellationToken cancellationToken)
        {
            user.Email = email;

            return Task.FromResult(0);
        }

        public Task<string> GetEmailAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.EmailConfirmed);
        }

        public Task SetEmailConfirmedAsync(ApplicationUserIdentity user, bool confirmed, CancellationToken cancellationToken)
        {
            user.EmailConfirmed = confirmed;
            return Task.FromResult(0);
        }

        public async Task<ApplicationUserIdentity> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            var output = await _db.LoadData<ApplicationUserIdentity, dynamic>("dbo.spApplicationUser_FindByNormalizedEmail", new { NormalizedEmail = normalizedEmail }, _connectionString.Name, cancellationToken);

            return output.First();
        }

        public Task<string> GetNormalizedEmailAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.NormalizedEmail);
        }

        public Task SetNormalizedEmailAsync(ApplicationUserIdentity user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;

            return Task.FromResult(0);
        }


        // phonenumberstore methods
        public Task SetPhoneNumberAsync(ApplicationUserIdentity user, string phoneNumber, CancellationToken cancellationToken)
        {
            user.PhoneNumber = phoneNumber;

            return Task.FromResult(0);
        }

        public Task<string> GetPhoneNumberAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumber);
        }

        public Task<bool> GetPhoneNumberConfirmedAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PhoneNumberConfirmed);
        }

        public Task SetPhoneNumberConfirmedAsync(ApplicationUserIdentity user, bool confirmed, CancellationToken cancellationToken)
        {
            user.PhoneNumberConfirmed = confirmed;

            return Task.FromResult(0);
        }


        // TwoFactorStore Methods
        public Task SetTwoFactorEnabledAsync(ApplicationUserIdentity user, bool enabled, CancellationToken cancellationToken)
        {
            user.TwoFactorEnabled = enabled;

            return Task.FromResult(0);
        }

        public Task<bool> GetTwoFactorEnabledAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.TwoFactorEnabled);
        }


        // passwordstore methods
        public Task SetPasswordHashAsync(ApplicationUserIdentity user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;

            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(ApplicationUserIdentity user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.PasswordHash is not null);
        }
    }
}
