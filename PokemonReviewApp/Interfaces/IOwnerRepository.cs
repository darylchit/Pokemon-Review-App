using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface IOwnerRepository
    {
        ICollection<Owner> GetOwners(); // We use ICollection for more features than IEnumberable
        Owner GetOwner(int ownerId);
        ICollection<Owner> GetOwnerOfAPokemon(int PokeId);
        ICollection<Pokemon> GetPokemonByOwner(int ownerId);
        bool OwnerExists(int ownerId);
        bool CreateOwner(Owner owner); // with CREATE pass in entire entity/object and EF will take care of it
        bool Save();
    }
}
