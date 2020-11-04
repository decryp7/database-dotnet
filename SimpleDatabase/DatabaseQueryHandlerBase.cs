using SimpleDatabase.SanityCheck;

namespace SimpleDatabase
{
    public abstract class
        DatabaseQueryHandlerBase<TDatabase, TConnection, TDatabaseQuery, TDatabaseQueryResult> : 
            DisposableObject,
            IDatabaseQueryHandler<TDatabase, TConnection, TDatabaseQuery, TDatabaseQueryResult>
        where TDatabaseQuery : IDatabaseQuery<TDatabase, TDatabaseQueryResult>
        where TDatabase : class, IDatabase<TDatabase>
    {
        public abstract TDatabaseQueryResult Handle(TConnection connection, TDatabaseQuery query);

        public TDatabase Database { get; set; }

        public object Handle(object connection, object query)
        {
            Guard.Ensure(connection, nameof(connection)).IsNotNull();
            Guard.Ensure(query, nameof(query)).IsNotNull();

            return Handle((TConnection)connection, (TDatabaseQuery)query);
        }
    }
}