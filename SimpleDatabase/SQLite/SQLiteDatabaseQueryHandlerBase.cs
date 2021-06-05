using System.Data.SQLite;
using System.Threading.Tasks;

namespace SimpleDatabase.SQLite
{
    public abstract class SQLiteDatabaseQueryHandlerBase<TDatabaseQuery, TDatabaseQueryResult> : 
        IDatabaseQueryHandler<SQLiteConnection, TDatabaseQuery, TDatabaseQueryResult>
        where TDatabaseQuery : IDatabaseQuery<TDatabaseQuery, TDatabaseQueryResult>
    {
        public abstract Task<TDatabaseQueryResult> Handle(SQLiteConnection connection, TDatabaseQuery databaseQuery);
    }
}