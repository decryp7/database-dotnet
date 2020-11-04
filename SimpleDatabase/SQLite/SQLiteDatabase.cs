using System.Collections.Generic;
using System.Data.SQLite;

namespace SimpleDatabase.SQLite
{
    public abstract class SQLiteDatabase<TDatabase> : Database<TDatabase, SQLiteConnection>
        where TDatabase : class, IDatabase<TDatabase>
    {
        protected override SQLiteConnection Initialize()
        {
            SQLiteConnectionStringBuilder connectionStringBuilder = new SQLiteConnectionStringBuilder
            {
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

            //Temp database, Benefit of using a Temp database is that the file automatically deleted even if process crash
            connectionStringBuilder.Add("FullUri", "file:");

            //Comment out to write to actual file
            //connectionStringBuilder.DataSource = Path.Combine(Path.GetTempPath(),
            //    FormattableString.Invariant($"{Path.GetRandomFileName()}.db"));

            string connectionString = connectionStringBuilder.ToString();
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            connection.DisposeWith(this);

            return connection;
        }

        protected SQLiteDatabase(IEnumerable<IDatabaseQueryHandler<TDatabase>> handlers) : base(handlers)
        {
        }
    }
}