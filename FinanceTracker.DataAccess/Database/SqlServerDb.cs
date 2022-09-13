using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTracker.DataAccess.Database
{
    public class SqlServerDb : IDatabaseAccess
    {
        private readonly IConfiguration _config;

        public SqlServerDb(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<T>(sqlStatement, parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task SaveData<T>(string sqlStatement, T parameters, string connectionStringName)
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                await connection.QueryAsync(sqlStatement, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
