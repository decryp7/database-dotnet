using System;
using System.Threading.Tasks;

namespace SimpleDatabase
{
    /// <summary>
    /// Database query handler interface
    /// </summary>
    /// <typeparam name="TConnection">Connection type</typeparam>
    /// <typeparam name="TDatabaseQuery">Database query type</typeparam>
    /// <typeparam name="TDatabaseQueryResult">Database query result type</typeparam>
    public interface IDatabaseQueryHandler<TConnection, TDatabaseQuery, TDatabaseQueryResult>
        : IDatabaseQueryHandler
        where TDatabaseQuery : IDatabaseQuery<TDatabaseQuery, TDatabaseQueryResult>
    {
        /// <summary>
        /// Handle the query
        /// </summary>
        /// <param name="connection">Database connection</param>
        /// <param name="databaseQuery">Database query</param>
        /// <returns>Database query result</returns>
        Task<TDatabaseQueryResult> Handle(TConnection connection, TDatabaseQuery databaseQuery);
    }

    /// <summary>
    /// Database query handler interface
    /// </summary>
    public interface IDatabaseQueryHandler
    {
    }
}