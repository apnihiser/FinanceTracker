using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using FinanceTracker.DataAccess.Database;
using FinanceTracker.DataAccess.Models;

namespace FinanceTracker.DataAccess.Data
{
    public class PayorData : IPayorData
    {
        private readonly IDatabaseAccess _db;
        private readonly ConnectionStringData _connectionString;

        public PayorData(IDatabaseAccess db, ConnectionStringData connectionString)
        {
            _db = db;
            _connectionString = connectionString;
        }

        public async Task<List<ApplicationUser>> GetAllPayors()
        {
            var rows = await _db.LoadData<ApplicationUser, dynamic>("dbo.spPayor_GetAll", new { }, _connectionString.Name);
            return rows.ToList();
        }

        public async Task<ApplicationUser?> GetPayorById(int id)
        {
            var row = await _db.LoadData<ApplicationUser, dynamic>("spPayor_GetById", new { Id = id }, _connectionString.Name);

            return row.FirstOrDefault();
        }

        public async Task<int> CreatePayor(string? firstName, string? lastName)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("FirstName", firstName);
            p.Add("LastName", lastName);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _db.SaveData("dbo.spPayor_Insert", p, _connectionString.Name);

            return p.Get<int>("Id");
        }

        public async Task DeletePayor(int id)
        {
            await _db.SaveData("dbo.spPayor_Delete", new { Id = id }, _connectionString.Name);
        }

        public async Task UpdatePayor(int id, string? firstName, string? lastName)
        {
            await _db.SaveData("dbo.spPayor_Update", new { Id = id, FirstName = firstName, LastName = lastName }, _connectionString.Name);
        }
    }
}
