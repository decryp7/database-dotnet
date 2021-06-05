using System;
using System.Threading.Tasks;

namespace SimpleDatabase
{
    /// <summary>
    /// Database interface
    /// </summary>
    public interface IDatabase : IDisposable
    {
        /// <summary>
        /// Query database
        /// </summary>
        /// <typeparam name="TDatabaseQuery">Database query type</typeparam>
        /// <typeparam name="TDatabaseQueryResult">Database result type</typeparam>
        /// <param name="databaseQuery">Database query</param>
        /// <returns></returns>
        Task<TDatabaseQueryResult> Execute<TDatabaseQuery, TDatabaseQueryResult>(
            IDatabaseQuery<TDatabaseQuery, TDatabaseQueryResult> databaseQuery)
            where TDatabaseQuery : IDatabaseQuery<TDatabaseQuery, TDatabaseQueryResult>;
    }

    /// <summary>
    /// Database interface
    /// </summary>
    /// <typeparam name="TConnection"></typeparam>
    public interface IDatabase<TConnection> : IDatabase
    {
        /// <summary>
        /// Get the database connection
        /// </summary>
        TConnection Connection { get; }
    }
}
