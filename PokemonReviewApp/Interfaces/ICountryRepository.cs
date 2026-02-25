using PokemonReviewApp.Models;

namespace PokemonReviewApp.Interfaces
{
    public interface ICountryRepository
    {
        ICollection<Country> GetCountries(); // Use ICollection instead of IEnumerable for better performance, IEnumberable is used for filtering
        Country GetCountry(int id); // Get a specific country by its ID, shows individual country details
        Country GetCountryByOwner(int ownerId); // Get a country based on the owner ID, useful for finding the country associated with a specific owner
        ICollection<Owner> GetOwnersFromACountry(int countryId); // Get a list of owners from a specific country, useful for finding all owners associated with a specific country
        bool CountryExists(int id); // Check if a country exists by its ID, useful for validating input and ensuring that operations are performed on valid countries
    }
}
