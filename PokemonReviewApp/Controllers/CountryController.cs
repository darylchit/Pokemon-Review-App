using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    public class CountryController : Controller
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper) // Inject Repository and AutoMapper into controller
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet] // 4. The GetPokemons method is decorated with the [HttpGet] attribute, indicating that it will handle HTTP GET requests.
        [ProducesResponseType(200, Type = typeof(IEnumerable<Country>))]

        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries); // It checks if the model state is valid. If it is not valid, it returns a BadRequest response with the model state
                                  // errors.
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(Category))]
        [ProducesResponseType(400)]

        public IActionResult GetCategory(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId)) // Validations
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }
        [HttpGet("owner/{ownerId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(Country))]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>
                (_countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }
    }
}
