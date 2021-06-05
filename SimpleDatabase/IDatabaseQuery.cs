namespace SimpleDatabase
{
    /// <summary>
    /// IDatabase query interface
    /// </summary>
    /// <typeparam name="TDatabaseQuery">Database query type</typeparam>
    /// <typeparam name="TDatabaseQueryResult">Database result type</typeparam>
    public interface IDatabaseQuery<TDatabaseQuery, TDatabaseQueryResult>
        where TDatabaseQuery : IDatabaseQuery<TDatabaseQuery, TDatabaseQueryResult>
    {
        
    }
}