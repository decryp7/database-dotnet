using SimpleDatabase;
using SimpleDatabase.SanityCheck;

namespace AnimalDatabase.Query
{
    public class AddAnimalQuery : IDatabaseQuery<AnimalDB, int>
    {
        public string Type { get; }
        public string Animal { get; }

        public AddAnimalQuery(string type, string animal)
        {
            Guard.Ensure(type, nameof(type)).IsNotNullOrEmpty();
            Guard.Ensure(animal, nameof(animal)).IsNotNullOrEmpty();

            Type = type;
            Animal = animal;
        }
    }
}