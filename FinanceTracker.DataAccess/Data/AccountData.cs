using Dapper;
using FinanceTracker.DataAccess.Database;
using FinanceTracker.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataAccess.Data
{
    public class AccountData : IAccountData
    {
        private readonly IDatabaseAccess _db;
        private readonly ConnectionStringData _connectionString;

        public AccountData(IDatabaseAccess db, ConnectionStringData connectionString)
        {
            _db = db;
            _connectionString = connectionString;
        }

        public async Task<List<AccountModel>> GetAllAccounts()
        {
            var output = await _db.LoadData<AccountModel, dynamic>("dbo.spAccount_GetAll", new { }, _connectionString.Name);

            return output.ToList();
        }

        public async Task<AccountModel?> GetAccountsByUserId(int id)
        {
            var output = await _db.LoadData<AccountModel, dynamic>("dbo.spAccount_GetById", new { Id = id }, _connectionString.Name);

            return output.First();
        }

        public async Task<int> Create(string? title, string? description, string? type, decimal balance, int holderId)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("Title", title);
            p.Add("Description", description);
            p.Add("Type", type);
            p.Add("Balance", balance);
            p.Add("ApplicationUserId", holderId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _db.SaveData("dbo.spAccount_Insert", p, _connectionString.Name);

            return p.Get<int>("Id");
        }

        public async Task Update(AccountModel accountRecord)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("Title", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));
            dataTable.Columns.Add("Type", typeof(string));
            dataTable.Columns.Add("Balance", typeof(string));
            dataTable.Columns.Add("ApplicationUserId", typeof(int));

            dataTable.Rows.Add(
                accountRecord.Id,
                accountRecord.Title,
                accountRecord.Description,
                accountRecord.Type,
                accountRecord.Balance,
                accountRecord.ApplicationUserId);

            await _db.SaveData("dbo.spAccount_Update", new { Account = dataTable.AsTableValuedParameter("dbo.AccountType") }, _connectionString.Name);
        }

        public async Task Delete(int id)
        {
            await _db.SaveData("dbo.spAccount_Delete", new { Id = id }, _connectionString.Name);
        }

        public async Task<List<FullAccountModel>> GetAllFullAccounts()
        {
            var rows = await _db.LoadData<FullAccountModel, dynamic>("dbo.spFullAccount_GetAll", new { }, _connectionString.Name);

            return rows.ToList();
        }

        public async Task<FullAccountModel> GetFullAccountByHolderId(int id)
        {
            var rows = await _db.LoadData<FullAccountModel, dynamic>("dbo.spFullAccount_GetById", new { Id = id }, _connectionString.Name);

            return rows.First();
        }
    }
}
