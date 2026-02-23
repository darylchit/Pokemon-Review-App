using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
// Repository is where you put your database calls
namespace PokemonReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository // Press Ctrl . to implement interface
    {
        private DataContext _context;
        public CategoryRepository(DataContext context)
        {
            _context = context;
        }
        public bool CategoryExists(int id)
        {
            return _context.Categories.Any(c => c.Id == id); // context = code that sits on top of DB and gives us access to DB
        } // Any is going to return bool, if it exists

        public ICollection<Category> GetCategories() //ICollection returns multiple
        {
            return _context.Categories.ToList();
        }

        public Category GetCategory(int id) // Category returns one, need FirstOrDefault if returning one
        {
            return _context.Categories.Where(e => e.Id == id).FirstOrDefault();
        }

        public ICollection<Pokemon> GetPokemonByCategory(int categoryId)
        {
            return _context.PokemonCategories.Where(e => e.CategoryId == categoryId).Select(c => c.Pokemon).ToList();
        }
    }
}