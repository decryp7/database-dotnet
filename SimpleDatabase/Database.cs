using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace SimpleDatabase
{
    public abstract class Database<TConnection> : DisposableObject, IDatabase<TConnection>
    {
        protected readonly IDictionary<Tuple<Type, Type, Type>, IDatabaseQueryHandler> DatabaseQueryHandlers =
            new Dictionary<Tuple<Type, Type, Type>, IDatabaseQueryHandler>();

        protected Database(IEnumerable<IDatabaseQueryHandler> dbQueryhandlers)
        {
            foreach (IDatabaseQueryHandler databaseQueryHandler in dbQueryhandlers)
            {
                Tuple<Type, Type, Type> queryType = FindQueryHandlerType(databaseQueryHandler.GetType());
                if (queryType != null)
                {
                    DatabaseQueryHandlers[queryType] = databaseQueryHandler;
                }
            }
        }

        private Tuple<Type, Type, Type> FindQueryHandlerType(Type baseType)
        {
            Type queryType = baseType.GetInterfaces().FirstOrDefault(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == typeof(IDatabaseQueryHandler<,,>));

            if (queryType != null)
            {
                return new Tuple<Type, Type, Type>(queryType.GenericTypeArguments[0], queryType.GenericTypeArguments[1],
                    queryType.GenericTypeArguments[2]);
            }

            return FindQueryHandlerType(baseType.BaseType);
        }

        public virtual async Task<TDatabaseQueryResult> Execute<TDatabaseQuery, TDatabaseQueryResult>(
            IDatabaseQuery<TDatabaseQuery, TDatabaseQueryResult> databaseQuery)
            where TDatabaseQuery : IDatabaseQuery<TDatabaseQuery, TDatabaseQueryResult>
        {
            if (DatabaseQueryHandlers.TryGetValue(
                new Tuple<Type, Type, Type>(typeof(TConnection), typeof(TDatabaseQuery), typeof(TDatabaseQueryResult)),
                out IDatabaseQueryHandler databaseQueryHandler))
            {
                return await
                    ((IDatabaseQueryHandler<TConnection, TDatabaseQuery, TDatabaseQueryResult>) databaseQueryHandler)
                    .Handle(Connection,
                        (TDatabaseQuery) databaseQuery);
            }

            return default(TDatabaseQueryResult);
        }

        public abstract TConnection Connection { get; }
    }
}