using System.Collections.Generic;
using System.Data.SQLite;
using SimpleDatabase.SQLite;

namespace AnimalDatabase.Query
{
    public class GetAnimalsQueryHandler :  SQLiteDatabaseQueryHandlerBase<AnimalDB, GetAnimalsQuery, IList<string>>
    {
        protected override IList<string> HandleImpl(SQLiteConnection connection, GetAnimalsQuery query)
        {
            IList<string> animals = new List<string>();

            using (SQLiteTransaction transaction = connection.BeginTransaction())
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = "select * from Animals where Type = @type";
                command.Parameters.Add(new SQLiteParameter("type", query.Type));

                using (SQLiteDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        animals.Add(dataReader["Name"].ToString());
                    }
                }
            }

            return animals;
        }
    }
}