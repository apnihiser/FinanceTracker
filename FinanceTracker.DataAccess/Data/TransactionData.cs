using FinanceTracker.DataAccess.Database;
using FinanceTracker.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataAccess.Data
{
    public class TransactionData : ITransactionData
    {
        private readonly IDatabaseAccess _db;
        private readonly ConnectionStringData _connectionString;

        public TransactionData(IDatabaseAccess db, ConnectionStringData connectionString)
        {
            _db = db;
            _connectionString = connectionString;
        }

        public async Task<List<TransactionModel>> GetAllFullTransactions()
        {
            var rows = await _db.LoadData<TransactionModel, dynamic>("dbo.spFullTransaction_GetAll", new { }, _connectionString.Name);

            return rows.ToList();
        }
    }
}
