using Dapper;
using FinanceTracker.DataAccess.Database;
using FinanceTracker.DataAccess.Models;
using Microsoft.Identity.Client;
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

        public async Task<List<TransactionModel>> GetFullTransactionsByProviderId(int providerId)
        {
            var rows = await _db.LoadData<TransactionModel, dynamic>("dbo.spFullTransactions_GetByProviderId", new { ProviderId = providerId }, _connectionString.Name);

            return rows.ToList();
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

        public async Task<List<TransactionModel>> GetUserTransactionsByMonth(string id, DateTime dateTime)
        {
            return await _db.LoadData<TransactionModel, dynamic>("dbo.spFullTransaction_GetByUserIdAndMonth", new { ApplicationUserId = id, TargetDate = dateTime }, _connectionString.Name);
        }

        public async Task<List<TransactionProviderAmount>> GetTransactionProviderChartDataByMonth(string id, DateTime dateTime, string transactionType)
        {
            return await _db.LoadData<TransactionProviderAmount, dynamic>("dbo.spTransactionProviderCost_GetByUserIdAndMonth", new { ApplicationUserId = id, TargetDate = dateTime, Type = transactionType }, _connectionString.Name);
        }

        public async Task<List<TransactionStatusAmount>> GetTransactionStatusChartDataByMonth(string id, DateTime dateTime)
        {
            return await _db.LoadData<TransactionStatusAmount, dynamic>("dbo.spTransactionStatusCost_GetByUserIdAndMonth", new { ApplicationUserId = id, TargetDate = dateTime }, _connectionString.Name);
        }

        public async Task<List<TransactionStatusCount>> GetTransactionStatusCountChartDataByMonth(string id, DateTime dateTime)
        {
            return await _db.LoadData<TransactionStatusCount, dynamic>("dbo.spTransactionStatusCount_GetByUserIdAndMonth", new { ApplicationUserId = id, TargetDate = dateTime }, _connectionString.Name);
        }

        public async Task DeleteTransactionById(int id)
        {
            await _db.SaveData("dbo.spFullTransaction_Delete", new { Id = id }, _connectionString.Name);
        }

        public async Task DeleteTransactionByAccountId(int accountId)
        {
            await _db.SaveData("dbo.spTransaction_DeleteByAccountId", new { AccountId = accountId }, _connectionString.Name);
        }

         public async Task DeleteTransactionByProviderId(int providerId)
        {
            await _db.SaveData("dbo.spTransaction_DeleteByProviderId", new { ProviderId = providerId }, _connectionString.Name);
        }

        public async Task EditTransactionById(TransactionModel record)
        {
            DataTable dataTable = new DataTable();

            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("AccountId", typeof(int));
            dataTable.Columns.Add("PayeeId", typeof(int));
            dataTable.Columns.Add("TransactionReason", typeof(string));
            dataTable.Columns.Add("Type", typeof(string));
            dataTable.Columns.Add("Amount", typeof(decimal));
            dataTable.Columns.Add("DueDate", typeof(DateTime));
            dataTable.Columns.Add("Status", typeof(string));

            dataTable.Rows.Add(
                record.Id,
                record.AccountId,
                record.PayeeId,
                record.TransactionReason,
                record.Type,
                record.Amount,
                record.DueDate,
                record.Status);

            await _db.SaveData("dbo.spFullTransaction_Edit", new { TransactionType = dataTable.AsTableValuedParameter("dbo.TransactionType") }, _connectionString.Name);
        }

        public async Task<int> CreateTransaction(TransactionModel input)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("AccountId", input.AccountId);
            p.Add("PayeeId", input.PayeeId);
            p.Add("TransactionReason", input.TransactionReason);
            p.Add("Type", input.Type);
            p.Add("Amount", input.Amount);
            p.Add("DueDate", input.DueDate);
            p.Add("Status", input.Status);
            p.Add("Id", DbType.Int32, direction: ParameterDirection.Output);

            await _db.SaveData("dbo.spFullTransaction_Insert", p, _connectionString.Name);

            return p.Get<int>("Id");
        }
    }
}
