namespace PokemonReviewApp.Models
{
    public class Pokemon
    {
        public int Id { get; set; } // We want people to see these three properties when they get a Pokemon,
                                    // so we put them in the Dto class, which is the Data Transfer Object for the Pokemon entity.
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public ICollection<Review> Reviews { get; set; } // We don't want people to see these properties
                                                         // so we don't put them in the PokemonDto class,
                                                         // which is the Data Transfer Object for the Pokemon entity.
        public ICollection<PokemonOwner> PokemonOwners { get; set; } // Many to many
        public  ICollection<PokemonCategory> PokemonCategories { get; set; } // Many to many
    }
}
