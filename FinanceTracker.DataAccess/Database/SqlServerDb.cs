using Dapper;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace FinanceTracker.DataAccess.Database
{
    public class SqlServerDb : IDatabaseAccess
    {
        private readonly IConfiguration _config;

        public SqlServerDb(IConfiguration config)
        {
            _config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName, CancellationToken cancellationToken = default(CancellationToken))
        {
            string connectionString = _config.GetConnectionString(connectionStringName);

            using (IDbConnection connection = new SqlConnection(connectionString))
            {
                var result = await connection.QueryAsync<T>(sqlStatement, parameters, commandType: CommandType.StoredProcedure);

                return result.ToList();
            }
        }

        public async Task SaveData<T>(string sqlStatement, T parameters, string connectionStringName, CancellationToken cancellationToken = default(CancellationToken))
        {
            cancellationToken.ThrowIfCancellationRequested();

            string connectionString = _config.GetConnectionString(connectionStringName);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync(cancellationToken);

                await connection.ExecuteAsync(sqlStatement, parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
