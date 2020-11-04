using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using AnimalDatabase.Query;
using SimpleDatabase;

namespace AnimalDatabase
{
    class Program
    {
        static void Main(string[] args)
        {
            IDatabase<AnimalDB> animalDB =
                new AnimalDB(new List<IDatabaseQueryHandler<AnimalDB>>
                {
                    new AddAnimalQueryHandler(),
                    new GetAnimalsQueryHandler()
                });

            animalDB.Query(new AddAnimalQuery("mammals", "cat")).Wait();
            animalDB.Query(new AddAnimalQuery("mammals", "bat")).Wait();
            animalDB.Query(new AddAnimalQuery("mammals", "elephant")).Wait();
            animalDB.Query(new AddAnimalQuery("mammals", "rabbit")).Wait();

            animalDB.Query(new AddAnimalQuery("birds", "chicken")).Wait();
            animalDB.Query(new AddAnimalQuery("birds", "falcon")).Wait();
            animalDB.Query(new AddAnimalQuery("birds", "flamingo")).Wait();
            animalDB.Query(new AddAnimalQuery("birds", "ostrich")).Wait();

            Console.WriteLine("Mammals: "+ string.Join(", ", animalDB.Query(new GetAnimalsQuery("mammals")).Wait()));
            Console.WriteLine("Birds: "+ string.Join(", ", animalDB.Query(new GetAnimalsQuery("birds")).Wait()));

            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);
        }
    }
}
