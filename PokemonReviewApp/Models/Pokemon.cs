namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; } // One to many
        public ICollection<PokemonOwner> PokemonOwners { get; set; } // Many to many
        public  ICollection<PokemonCategory> PokemonCategories { get; set; } // Many to many
    }
}
