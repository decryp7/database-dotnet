using System.Collections.Generic;
using System.Data.SQLite;
using System.Threading.Tasks;
using SimpleDatabase.SQLite;

namespace AnimalDatabase.Query
{
    public class GetAnimalsQueryHandler : 
        SQLiteDatabaseQueryHandlerBase<GetAnimalsQuery, IList<string>>
    {
        public override Task<IList<string>> Handle(SQLiteConnection connection, GetAnimalsQuery databaseQuery)
        {
            return Task.Run(() =>
            {
                IList<string> animals = new List<string>();

                using (SQLiteTransaction transaction = connection.BeginTransaction())
                using (SQLiteCommand command = connection.CreateCommand())
                {
                    command.CommandText = "select * from Animals where Type = @type";
                    command.Parameters.Add(new SQLiteParameter("type", databaseQuery.Type));

                    using (SQLiteDataReader dataReader = command.ExecuteReader())
                    {
                        while (dataReader.Read())
                        {
                            animals.Add(dataReader["Name"].ToString());
                        }
                    }
                }

                return animals;
            });
        }
    }
}