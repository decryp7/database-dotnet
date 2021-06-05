using System.Collections.Generic;
using GuardLibrary;
using SimpleDatabase;


namespace AnimalDatabase.Query
{
    public class GetAnimalsQuery : IDatabaseQuery<GetAnimalsQuery, IList<string>>
    {
        public string Type { get; }

        public GetAnimalsQuery(string type)
        {
            Guard.Ensure(type, nameof(type)).IsNotNullOrEmpty();
            Type = type;
        }
    }
}