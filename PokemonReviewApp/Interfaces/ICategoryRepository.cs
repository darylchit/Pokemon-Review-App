using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICategoryRepository // Blueprint for the CategoryRepository class, which will implement this interface.
                                         // It defines the contract for the repository, specifying the methods that must be
                                         // implemented to interact with the Category entity in the database.
    {
        // with READ you pass in identifiers to pull info
        ICollection<Category> GetCategories(); // Get all categories
        Category GetCategory(int id); // Return a category by its id
        ICollection<Pokemon> GetPokemonByCategory(int categoryId); // Return Pokemon by category id
        bool CategoryExists(int id); // Check if a category exists by its id

        // signatures

        bool CreateCategory(Category category); // with CREATE pass in entire entity/object and EF will take care of it
        bool Save();

    }
}
