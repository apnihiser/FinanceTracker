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
    public class ProviderData : IProviderData
    {
        private readonly IDatabaseAccess _db;
        private readonly ConnectionStringData _connectionString;

        public ProviderData(IDatabaseAccess db, ConnectionStringData connectionString)
        {
            _db = db;
            _connectionString = connectionString;
        }

        public async Task<List<ProviderModel>> GetAllProviders()
        {
            var rows = await _db.LoadData<ProviderModel, dynamic>("dbo.spProvider_GetAll", new { }, _connectionString.Name);
            return rows.ToList();
        }

        public async Task<ProviderModel?> GetProviderById(int id)
        {
            var row = await _db.LoadData<ProviderModel, dynamic>("dbo.spProvider_GetById", new { Id = id }, _connectionString.Name);
            return row.FirstOrDefault();
        }

        public async Task<int> CreateProvider(ProviderModel model)
        {
            DynamicParameters p = new DynamicParameters();

            p.Add("Title", model.Title);
            p.Add("Service", model.Service);
            p.Add("URL", model.Url);
            p.Add("PayorId", model.PayorId);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _db.SaveData("dbo.spProvider_Insert", p, _connectionString.Name);

            return p.Get<int>("Id");
        }

        public async Task DeleteProvider(int id)
        {
            await _db.SaveData("dbo.spProvider_Delete", new { Id = id }, _connectionString.Name);
        }

        public async Task UpdateProvider(int id, string? service, string? title, string? url)
        {
            await _db.SaveData("spProvider_Update", new { Id = id, Title = title, Service = service, URL = url }, _connectionString.Name);
        }
    }
}
