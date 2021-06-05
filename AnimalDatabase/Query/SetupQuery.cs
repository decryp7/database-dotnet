using SimpleDatabase;

namespace AnimalDatabase.Query
{
    public class SetupQuery : IDatabaseQuery<SetupQuery, bool>
    {
        public string SQL =>
            "create table if not exists Animals (Type, Name);" +
            "create index if not exists Animals_TypeIndex on Animals (Type);";
    }
}