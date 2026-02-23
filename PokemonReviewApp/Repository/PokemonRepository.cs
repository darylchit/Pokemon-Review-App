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

        public Pokemon GetPokemon(int id) // These come from the IPokemonRepository interface, which defines the contract for the repository.
                                          // They appeared
                                          // after implementing the interface, The IPokemonRepository on line 7 was red and I clicked implement interface
                                          // and we have to provide implementations for all the
                                          // methods defined in the interface.
        {
            return _context.Pokemons.Where(p => p.Id == id).FirstOrDefault(); // Goes into our context and
                                                                              // looks for the Pokemons table and finds the first Pokemon
                                                                              // with the matching Id and returns it.
                                                                              // If no Pokemon is found, it returns null.
        }

        public Pokemon GetPokemon(string name)
        {
            return _context.Pokemons.Where(p => p.Name == name).FirstOrDefault(); // Similar to the previous method, but it searches for a
                                                                                  // Pokemon by its Name instead of Id.
        }

        public decimal GetPokemonRating(int pokeId)
        {
            var review = _context.Reviews.Where(p => p.Pokemon.Id == pokeId); // This method retrieves the reviews for a specific Pokemon based on its Id.
            if (review.Count() <= 0)
                return 0; // If there are no reviews for the Pokemon, it returns a rating of 0.

            return ((decimal)review.Sum(r => r.Rating) / review.Count()); // If there are reviews, it calculates the average rating by summing up the
                                                                         // ratings and dividing by the count of reviews, and returns the result as a decimal.
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

        public bool PokemonExists(int pokeId)
        {
            return _context.Pokemons.Any(p => p.Id == pokeId); // This method checks if a Pokemon with a specific Id exists
                                                               // in the database. It returns true if a Pokemon with the given
                                                               // Id exists, and false otherwise.
        }
    }
}
