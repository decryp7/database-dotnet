using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using SimpleDatabase.SanityCheck;

namespace SimpleDatabase
{
    public abstract class Database<TDatabase, TConnection> : DisposableObject, 
        IDatabase<TDatabase, TConnection>
        where TDatabase : class, IDatabase<TDatabase>
    {
        private TConnection connection;
        private bool isInitialized;
        private readonly object gate;
        private readonly IDictionary<Type, IDatabaseQueryHandler<TDatabase>> queryHandlers;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        protected Database(IEnumerable<IDatabaseQueryHandler<TDatabase>> handlers)
        {
            queryHandlers = new Dictionary<Type, IDatabaseQueryHandler<TDatabase>>();
            gate = new object();

            foreach (IDatabaseQueryHandler<TDatabase> handler in handlers)
            {
                Type queryType = FindQueryHandlerType(handler.GetType());

                RegisterQueryHandler(handler, queryType);
            }

            Disposable.Create(() =>
            {
                foreach (IDatabaseQueryHandler<TDatabase> queryHandler in queryHandlers.Values)
                {
                    queryHandler.Dispose();
                }
                queryHandlers.Clear();
            }).DisposeWith(this);
        }

        private Type FindQueryHandlerType(Type baseType)
        {
            Type queryType = baseType.GenericTypeArguments.FirstOrDefault(type => type.GetInterfaces().Any(x =>
                x.IsGenericType &&
                x.GetGenericTypeDefinition() == typeof(IDatabaseQuery<,>)));

            if (queryType == null)
            {
                return FindQueryHandlerType(baseType.BaseType);
            }

            return queryType;
        }

        public TConnection Connection
        {
            get
            {
                lock (gate)
                {
                    if (!isInitialized)
                    {
                        connection = Initialize();
                        isInitialized = true;
                    }

                    return connection;
                }
            }
        }

        protected abstract TConnection Initialize();

        public IObservable<TDatabaseQueryResult> Query<TDatabaseQueryResult>(IDatabaseQuery<TDatabase, TDatabaseQueryResult> query)
        {
            return Observable.Create<TDatabaseQueryResult>(observer =>
            {
                Type queryType = query.GetType();

                if (!queryHandlers.TryGetValue(queryType, out IDatabaseQueryHandler<TDatabase> queryHandler))
                {
                    throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture,
                        "{0} is unable to handle {1}.", this.GetType().GetFriendlyName(), queryType.Name));
                }

                observer.OnNext((TDatabaseQueryResult)queryHandler.Handle(Connection, query));
                observer.OnCompleted();
                return Disposable.Empty;
            });
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public void Register<TDatabaseQuery>(IDatabaseQueryHandler<TDatabase, TDatabaseQuery> queryHandler)
        {
            Guard.Ensure(queryHandler, nameof(queryHandler)).IsNotNull();

            Type queryType = typeof(TDatabaseQuery);

            RegisterQueryHandler(queryHandler, queryType);
        }

        private void RegisterQueryHandler(IDatabaseQueryHandler<TDatabase> queryHandler, Type queryType)
        {
            queryHandler.Database = this as TDatabase;

            if (queryHandlers.ContainsKey(queryType))
            {
                queryHandlers[queryType] = queryHandler;
            }
            else
            {
                queryHandlers.Add(queryType, queryHandler);
            }
        }
    }
}