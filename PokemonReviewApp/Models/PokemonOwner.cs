namespace PokemonReviewApp.Models
{
    public class PokemonOwner
    {
        public int PokemonId { get; set; }
        public int OwnerId { get; set; }
        public  Pokemon Pokemon { get; set; }
        public  Owner Owner { get; set; }
    }
}
// 3. Creating the PokemonOwner Model for Many-to-Many Relationships