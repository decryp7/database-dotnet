using System;

namespace SimpleDatabase
{
    /// <summary>
    /// Database query handler
    /// </summary>
    /// <typeparam name="TDatabase">Database type</typeparam>
    public interface IDatabaseQueryHandler<TDatabase> : IDisposable
        where TDatabase : class, IDatabase<TDatabase>
    {
        /// <summary>
        /// Get/set the database
        /// </summary>
        TDatabase Database { get; set; }

        /// <summary>
        /// Handle database query
        /// </summary>
        /// <param name="connection">Connection</param>
        /// <param name="query">Query</param>
        /// <returns>Query result</returns>
        object Handle(object connection, object query);
    }

    /// <summary>
    /// Database query handler
    /// </summary>
    /// <typeparam name="TDatabase">Database type</typeparam>
    /// <typeparam name="TDatabaseQuery">Database query type</typeparam>
    public interface IDatabaseQueryHandler<TDatabase, TDatabaseQuery> :
        IDatabaseQueryHandler<TDatabase>
        where TDatabase : class, IDatabase<TDatabase>
    {

    }

    /// <summary>
    /// Database query handler
    /// </summary>
    /// <typeparam name="TDatabase">Database type</typeparam>
    /// <typeparam name="TConnection">Connection type</typeparam>
    /// <typeparam name="TDatabaseQuery">Database query type</typeparam>
    /// <typeparam name="TDatabaseQueryResult">Database query result type</typeparam>
    public interface IDatabaseQueryHandler<TDatabase, TConnection, TDatabaseQuery, TDatabaseQueryResult> : 
        IDatabaseQueryHandler<TDatabase, TDatabaseQuery>
        where TDatabaseQuery : IDatabaseQuery<TDatabase, TDatabaseQueryResult>
        where TDatabase : class, IDatabase<TDatabase>
    {
        /// <summary>
        /// Handler database query
        /// </summary>
        /// <param name="connection">Connection</param>
        /// <param name="query">Query</param>
        /// <returns>Query result</returns>
        TDatabaseQueryResult Handle(TConnection connection, TDatabaseQuery query);
    }
}