using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository // 5. Repository Pattern & Dependency Injection Explained
{
    public class PokemonRepository : IPokemonRepository // 1. A repository is a class that handles data access logic for a specific entity,
                                   // in this case, Pokemon. It provides methods to perform CRUD
                                   // (Create, Read, Update, Delete) operations on the Pokemon entity.
    {
        private readonly DataContext _context; // 3. The repository has a private readonly field called _context
                                               // of type DataContext. This field is used to interact with the
                                               // database and perform operations on the Pokemon entity.
        public PokemonRepository(DataContext context) // 2. The constructor of the PokemonRepository class takes
                                                      // a DataContext object as a parameter and assigns it to a
                                                      // private readonly field called _context. This allows the
                                                      // repository to interact with the database through the
                                                      // DataContext.
        {
            _context = context; // 4. The constructor initializes the _context field with the provided
                                // DataContext object, allowing the repository to use it for database operations.
        }

        public ICollection<Pokemon> GetPokemons() // 5. The GetPokemons method is a public method that returns
                                                  // a collection of Pokemon objects.
        {
            return _context.Pokemons.OrderBy(p => p.Id).ToList(); // 6. Inside the GetPokemons method, it
                                                                  // retrieves all Pokemon entities from the database
                                                                  // using the _context.Pokemons property, orders them
                                                                  // by their Id property,
                                                                  // and converts the result to a list before
                                                                  // returning it.
        }
    }
}
