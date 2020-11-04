using System.Data.SQLite;

namespace SimpleDatabase.SQLite
{
    public abstract class
        SQLiteDatabaseQueryHandlerBase<TDatabase, TDatabaseQuery, TDatabaseQueryResult> :
            DatabaseQueryHandlerBase<TDatabase, SQLiteConnection, TDatabaseQuery, TDatabaseQueryResult>
        where TDatabaseQuery : IDatabaseQuery<TDatabase, TDatabaseQueryResult>
        where TDatabase : class, IDatabase<TDatabase>
    {
        public override TDatabaseQueryResult Handle(SQLiteConnection connection, TDatabaseQuery query)
        {
            return HandleImpl(connection, query);
        }

        protected abstract TDatabaseQueryResult HandleImpl(SQLiteConnection connection, TDatabaseQuery query);
    }
}