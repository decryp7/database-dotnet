using System.Collections.Generic;
using SimpleDatabase;
using SimpleDatabase.SanityCheck;

namespace AnimalDatabase.Query
{
    public class GetAnimalsQuery : IDatabaseQuery<AnimalDB, IList<string>>
    {
        public string Type { get; }

        public GetAnimalsQuery(string type)
        {
            Guard.Ensure(type, nameof(type)).IsNotNullOrEmpty();
            Type = type;
        }
    }
}