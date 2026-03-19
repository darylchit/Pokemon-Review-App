using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class OwnerRepository : IOwnerRepository
    {

        private readonly DataContext _context;
        public OwnerRepository(DataContext context)
        {
            _context = context;
        }

        public bool CreateOwner(Owner owner)
        {
            _context.Add(owner); // Add the provided owner object to the database context, marking it for insertion into the database when changes are saved.
            return Save(); // Call the Save method to persist the changes to the database and return its result.
        }

        public Owner GetOwner(int ownerId) 
        {
            return _context.Owners.Where(o => o.Id == ownerId).FirstOrDefault(); 
        }

        public ICollection<Owner> GetOwnerOfAPokemon(int pokeId)
        {
            return _context.PokemonOwners.Where(p => p.Pokemon.Id == pokeId).Select(o => o.Owner).ToList();
        }

        public ICollection<Owner> GetOwners()
        {
            return _context.Owners.ToList();
        }

        public ICollection<Pokemon> GetPokemonByOwner(int ownerId)
        {
            return _context.PokemonOwners.Where(p => p.Owner.Id == ownerId).Select(p => p.Pokemon).ToList();
        }

        public bool OwnerExists(int ownerId)
        {
            return _context.Owners.Any(o => o.Id == ownerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); // Save the changes made to the database context, returning the number of state entries written to the database.
            return saved > 0 ? true : false; // Return true if one or more entries were saved to the database, indicating that the save operation was successful, and false otherwise.
        }
    }
}
