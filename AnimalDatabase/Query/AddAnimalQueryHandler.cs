using System.Data.SQLite;
using System.Threading.Tasks;
using SimpleDatabase.SQLite;

namespace AnimalDatabase.Query
{
    public class AddAnimalQueryHandler : 
        SQLiteDatabaseQueryHandlerBase<AddAnimalQuery, int>
    {
        public override Task<int> Handle(SQLiteConnection connection, AddAnimalQuery databaseQuery)
        {
            return Task.Run(() =>
            {
                int result = 0;

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "insert into Animals values (@type, @animal)";
                    command.Parameters.Add(new SQLiteParameter("type", databaseQuery.Type));
                    command.Parameters.Add(new SQLiteParameter("animal", databaseQuery.Animal));

                    result += command.ExecuteNonQuery();
                    transaction.Commit();
                }

                return result;
            });
        }
    }
}