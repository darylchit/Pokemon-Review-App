using AutoMapper;
using PokemonReviewApp.Data;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Repository
{
    public class CountryRepository : ICountryRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public CountryRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public bool CountryExists(int id)
        {
            return _context.Countries.Any(c => c.Id == id); // Check if any country in the database has the specified ID, returning true if it exists and false otherwise.
        }

        public bool CreateCountry(Country country)
        {
            _context.Add(country); // Add the provided country object to the database context, marking it for insertion into the database when changes are saved.
            return Save();
        }

        public ICollection<Country> GetCountries()
        {
            return _context.Countries.ToList(); // Retrieve all countries from the database and return them as a list.
        }

        public Country GetCountry(int id)
        {
            return _context.Countries.Where(c => c.Id == id).FirstOrDefault(); // Retrieve a single country from the database that matches the specified ID, returning null if no match is found.
        }

        public Country GetCountryByOwner(int ownerId)
        {
            return _context.Owners.Where(o => o.Id == ownerId).Select(o => o.Country).FirstOrDefault(); // Retrieve the country associated with a specific owner by filtering the owners based on the provided owner ID, selecting the associated country, and returning it. If no match is found, it returns null.).Select(c => c.Country).FirstOrDefault();
        }

        public ICollection<Owner> GetOwnersFromACountry(int countryId)
        {
            return _context.Owners.Where(c => c.Country.Id == countryId).ToList(); // Retrieve all owners from the database whose associated country's ID matches the specified country ID, returning them as a list.
        }

        public bool Save()
        {
            var saved = _context.SaveChanges(); // Save the changes made to the database context, returning the number of state entries written to the database.
            return saved > 0 ? true : false; // Return true if one or more entries were saved to the database, indicating that the save operation was successful, and false otherwise.
        }
    }
}
