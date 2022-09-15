namespace FinanceTracker.DataAccess.Database
{
    public interface IDatabaseAccess
    {
        Task<List<T>> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName, CancellationToken cancellationToken = default);
        Task SaveData<T>(string sqlStatement, T parameters, string connectionStringName, CancellationToken cancellationToken = default);
    }
}