using System;

namespace SimpleDatabase
{
    /// <summary>
    /// Database
    /// </summary>
    ///<typeparam name = "TDatabase" > Database type</typeparam>
    public interface IDatabase<TDatabase> : IDisposable
        where TDatabase : class, IDatabase<TDatabase>
    {
        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="TDatabaseQueryResult">Query result type</typeparam>
        /// <param name="query">Query</param>
        /// <returns>Query result</returns>
        IObservable<TDatabaseQueryResult> Query<TDatabaseQueryResult>(
            IDatabaseQuery<TDatabase, TDatabaseQueryResult> query);

        /// <summary>
        /// Register query handler
        /// </summary>
        /// <typeparam name="TDatabaseQuery">Query type</typeparam>
        /// <param name="queryHandler">Query handler</param>
        void Register<TDatabaseQuery>(IDatabaseQueryHandler<TDatabase, TDatabaseQuery> queryHandler);
    }

    /// <summary>
    /// Database
    /// </summary>
    /// <typeparam name="TDatabase">Database type</typeparam>
    /// <typeparam name="TConnection">Connection type</typeparam>
    public interface IDatabase<TDatabase,TConnection> : 
        IDatabase<TDatabase>
        where TDatabase : class, IDatabase<TDatabase>
    {
        /// <summary>
        /// Connection
        /// </summary>
        TConnection Connection { get; }
    }
}
