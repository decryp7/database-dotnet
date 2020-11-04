using System.Data.SQLite;
using SimpleDatabase.SQLite;

namespace AnimalDatabase.Query
{
    public class AddAnimalQueryHandler : 
        SQLiteDatabaseQueryHandlerBase<AnimalDB, AddAnimalQuery, int>
    {
        protected override int HandleImpl(SQLiteConnection connection, AddAnimalQuery query)
        {
            int result = 0;

            using (SQLiteTransaction transaction = connection.BeginTransaction())
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "insert into Animals values (@type, @animal)";
                command.Parameters.Add(new SQLiteParameter("type", query.Type));
                command.Parameters.Add(new SQLiteParameter("animal", query.Animal));

                result += command.ExecuteNonQuery();
                transaction.Commit();
            }

            return result;
        }
    }
}