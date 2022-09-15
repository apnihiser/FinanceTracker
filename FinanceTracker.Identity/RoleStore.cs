using Dapper;
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
    public class RoleStore : IRoleStore<ApplicationRole> 
    {
        private readonly IDatabaseAccess _db;
        private readonly ConnectionStringData _connectionString;

        public RoleStore(IDatabaseAccess db, ConnectionStringData connectionString)
        {
            _db = db;
            _connectionString = connectionString;
        }

        public async Task<IdentityResult> CreateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            DynamicParameters p = new();
            p.Add("Name", role.Name);
            p.Add("NormalizedName", role.NormalizedName);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.InputOutput);

            await _db.SaveData("dbo.spApplicationRole_Insert", p, _connectionString.Name, cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> UpdateAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            await _db.SaveData("dbo.spApplicationRole_UpdateById", new { Name = role.Name, NormalizedName = role.NormalizedName }, _connectionString.Name, cancellationToken);

            return IdentityResult.Success;
        }

        public async Task<IdentityResult> DeleteAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            await _db.SaveData("dbo.spApplicationRole_DeleteById", new { Id = role.Id }, _connectionString.Name, cancellationToken);

            return IdentityResult.Success;
        }

        public void Dispose()
        {
            // nothing to dispose
        }

        public Task<string> GetRoleIdAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Id.ToString());
        }

        public async Task<ApplicationRole> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            var output = await _db.LoadData<ApplicationRole, dynamic>("dbo.spApplicationRole_GetById", new { Id = roleId }, _connectionString.Name, cancellationToken);

            return output.First();
        }

        public async Task<ApplicationRole> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            var output = await _db.LoadData<ApplicationRole, dynamic>("dbo.spApplicationRole_GetByNormalizedName", new { NormalizedName = normalizedRoleName }, _connectionString.Name, cancellationToken);

            return output.First();
        }

        public Task<string> GetNormalizedRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.NormalizedName);
        }

        public Task<string> GetRoleNameAsync(ApplicationRole role, CancellationToken cancellationToken)
        {
            return Task.FromResult(role.Name);
        }

        public Task SetNormalizedRoleNameAsync(ApplicationRole role, string normalizedName, CancellationToken cancellationToken)
        {
            role.NormalizedName = normalizedName;

            return Task.FromResult(0);
        }

        public Task SetRoleNameAsync(ApplicationRole role, string roleName, CancellationToken cancellationToken)
        {
            role.Name = roleName;

            return Task.FromResult(0);
        }

        
    }
}
