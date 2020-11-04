using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SimpleDatabase;
using SimpleDatabase.SQLite;


namespace AnimalDatabase
{
    public class AnimalDB : SQLiteDatabase<AnimalDB>
    {
        public AnimalDB(IEnumerable<IDatabaseQueryHandler<AnimalDB>> handlers) : base(handlers)
        {
        }

        protected override SQLiteConnection Initialize()
        {
            SQLiteConnection connection = base.Initialize();

            using (SQLiteTransaction transaction = connection.BeginTransaction())
            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandText = FormattableString.Invariant($"create table if not exists Animals (Type, Name)");
                command.ExecuteNonQuery();

                command.CommandText = "create index Animals_TypeIndex on Animals (Type)";
                command.ExecuteNonQuery();

                transaction.Commit();
            }

            return connection;
        }
    }
}