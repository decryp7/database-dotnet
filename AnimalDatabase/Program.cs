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
            IDatabase animalDB =
                new AnimalDB("Animals.db",new List<IDatabaseQueryHandler>
                {
                    new SetupQueryHandler(),
                    new AddAnimalQueryHandler(),
                    new GetAnimalsQueryHandler()
                });

            if (!animalDB.Execute(new SetupQuery()).GetAwaiter().GetResult())
            {
                Console.WriteLine("Unable to setup database!");
            }
            else
            {
                animalDB.Execute(new AddAnimalQuery("mammals", "cat")).Wait();
                animalDB.Execute(new AddAnimalQuery("mammals", "bat")).Wait();
                animalDB.Execute(new AddAnimalQuery("mammals", "elephant")).Wait();
                animalDB.Execute(new AddAnimalQuery("mammals", "rabbit")).Wait();

                animalDB.Execute(new AddAnimalQuery("birds", "chicken")).Wait();
                animalDB.Execute(new AddAnimalQuery("birds", "falcon")).Wait();
                animalDB.Execute(new AddAnimalQuery("birds", "flamingo")).Wait();
                animalDB.Execute(new AddAnimalQuery("birds", "ostrich")).Wait();

                Console.WriteLine("Mammals: " +
                                  string.Join(", ", animalDB.Execute(new GetAnimalsQuery("mammals")).GetAwaiter().GetResult()));
                Console.WriteLine("Birds: " + string.Join(", ", animalDB.Execute(new GetAnimalsQuery("birds")).GetAwaiter().GetResult()));
            }

            Console.Write("\nPress any key to exit...");
            Console.ReadKey(true);
        }
    }
}
