using System;
using System.Collections.Generic;
using System.Data.SQLite;
using SimpleDatabase;
using SimpleDatabase.SQLite;


namespace AnimalDatabase
{
    public class AnimalDB : SQLiteDatabaseBase
    {
        public AnimalDB(string filePath, IEnumerable<IDatabaseQueryHandler> dbQueryhandlers) 
            : base(filePath, dbQueryhandlers)
        {
        }
    }
}