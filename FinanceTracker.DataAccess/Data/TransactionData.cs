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
    public class TransactionData : ITransactionData
    {
        private readonly IDatabaseAccess _db;
        private readonly ConnectionStringData _connectionString;

        public TransactionData(IDatabaseAccess db, ConnectionStringData connectionString)
        {
            _db = db;
            _connectionString = connectionString;
        }

        public async Task<TransactionModel> GetFullTransactionById(int id)
        {
            var rows = await _db.LoadData<TransactionModel, dynamic>("dbo.spFullTransaction_GetById", new { Id = id }, _connectionString.Name);

            return rows.FirstOrDefault();
        }

        public async Task<List<TransactionModel>> GetAllFullTransactions()
        {
            var rows = await _db.LoadData<TransactionModel, dynamic>("dbo.spFullTransaction_GetAll", new { }, _connectionString.Name);

            return rows.ToList();
        }

        public async Task<List<TransactionModel>> GetAllFullTransactionsByUserIdAsync(string id)
        {
            return await _db.LoadData<TransactionModel, dynamic>("dbo.spFullTransaction_GetByUserId", new { ApplicationUserId = id }, _connectionString.Name);
        }

        public async Task DeleteTransactionById(int id)
        {
            await _db.SaveData("dbo.spFullTransaction_Delete", new { Id = id }, _connectionString.Name);
        }

        public async Task EditTransactionById(TransactionModel record)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("AccountId", typeof(int));
            dataTable.Columns.Add("PayeeId", typeof(int));
            dataTable.Columns.Add("Amount", typeof(decimal));
            dataTable.Columns.Add("DueDate", typeof(DateTime));
            dataTable.Columns.Add("Status", typeof(string));

            dataTable.Rows.Add(
                record.Id,
                record.AccountId,
                record.PayeeId,
                record.Amount,
                record.DueDate,
                record.Status);

            await _db.SaveData("dbo.spFullTransaction_Edit", new { TransactionType = dataTable.AsTableValuedParameter("dbo.TransactionType") }, _connectionString.Name);
        }

        public async Task CreateTransaction(TransactionModel input)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("AccountId", typeof(int));
            dataTable.Columns.Add("PayeeId", typeof(int));
            dataTable.Columns.Add("Amount", typeof(decimal));
            dataTable.Columns.Add("DueDate", typeof(DateTime));
            dataTable.Columns.Add("Status", typeof(string));

            dataTable.Rows.Add(
                input.Id,
                input.AccountId,
                input.PayeeId,
                input.Amount,
                input.DueDate,
                input.Status);

            await _db.SaveData("dbo.spFullTransaction_Insert", new { TransactionType = dataTable.AsTableValuedParameter("dbo.TransactionType") }, _connectionString.Name);
        }
    }
}
