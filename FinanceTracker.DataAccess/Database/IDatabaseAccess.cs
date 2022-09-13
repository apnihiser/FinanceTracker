namespace FinanceTracker.DataAccess.Database
{
    public interface IDatabaseAccess
    {
        Task<List<T>> LoadData<T, U>(string sqlStatement, U parameters, string connectionStringName);
        Task SaveData<T>(string sqlStatement, T parameters, string connectionStringName);
    }
}