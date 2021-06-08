using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using GuardLibrary;

namespace SimpleDatabase.SQLite
{
    public abstract class SQLiteDatabaseBase : Database<SQLiteConnection>
    {
        private readonly string filePath;

        protected SQLiteDatabaseBase(string filePath, IEnumerable<IDatabaseQueryHandler> dbQueryhandlers) : base(dbQueryhandlers)
        {
            Guard.Ensure(filePath, nameof(filePath)).IsNotNullOrEmpty();
            this.filePath = filePath;
        }

        public override SQLiteConnection Connection => Connect();

        public override async Task<TDatabaseQueryResult> Execute<TDatabaseQuery, TDatabaseQueryResult>(IDatabaseQuery<TDatabaseQuery, TDatabaseQueryResult> databaseQuery)
        {
            if (DatabaseQueryHandlers.TryGetValue(
                new Tuple<Type, Type, Type>(typeof(SQLiteConnection), typeof(TDatabaseQuery), typeof(TDatabaseQueryResult)),
                out IDatabaseQueryHandler databaseQueryHandler))
            {
                using (SQLiteConnection connection = Connection)
                {
                    TDatabaseQueryResult result = await
                        ((IDatabaseQueryHandler<SQLiteConnection, TDatabaseQuery, TDatabaseQueryResult>)
                            databaseQueryHandler)
                        .Handle(connection,
                            (TDatabaseQuery) databaseQuery);
                    return result;
                }
            }

            return default(TDatabaseQueryResult);
        }

        private SQLiteConnection Connect()
        {
            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder
            {
                ForeignKeys = true,
                DataSource = filePath,
                //Refer to https://blog.devart.com/increasing-sqlite-performance.html
                JournalMode = SQLiteJournalModeEnum.Memory,
                Version = 3,
                PageSize = 4096,
                //Keep 200mb of memory cache, if db exceed 200mb, sqlite start to write to temp file.
                //200,000,000 / 4096 = 48828
                CacheSize = 48828,
                SyncMode = SynchronizationModes.Off
            };
            connectionStringBuilder.Add("PRAGMA locking_mode", "EXCLUSIVE");

            string connectionString = connectionStringBuilder.ToString();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();

            return connection;
        }
    }
}