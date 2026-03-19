using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using PokemonReviewApp.Repository;

namespace PokemonReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryController : ControllerBase
    {
        private readonly ICountryRepository _countryRepository;
        private readonly IMapper _mapper;

        public CountryController(ICountryRepository countryRepository, IMapper mapper) // Inject Repository and AutoMapper into controller
        {
            _countryRepository = countryRepository;
            _mapper = mapper;
        }

        [HttpGet] // 4. The GetPokemons method is decorated with the [HttpGet] attribute, indicating that it will handle HTTP GET requests.
        [ProducesResponseType(200, Type = typeof(IEnumerable<CountryDto>))]

        public IActionResult GetCountries()
        {
            var countries = _mapper.Map<List<CountryDto>>(_countryRepository.GetCountries());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(countries); // It checks if the model state is valid. If it is not valid, it returns a BadRequest response with the model state
                                  // errors.
        }

        [HttpGet("{countryId}")] // Decorator for the GetCategory method, indicating that it will handle
                                 // HTTP GET requests with a route parameter named categoryId. The method
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        [ProducesResponseType(400)]

        public IActionResult GetCountry(int countryId)
        {
            if (!_countryRepository.CountryExists(countryId)) // Validations
                return NotFound();

            var country = _mapper.Map<CountryDto>(_countryRepository.GetCountry(countryId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(country);
        }
        [HttpGet("/owners/{ownerId}")] // Decorator for the GetCountryOfAnOwner method, indicating that it will handle
                                       // HTTP GET requests with a route parameter named ownerId. The method retrieves the country associated with a specific owner.
        [ProducesResponseType(400)]
        [ProducesResponseType(200, Type = typeof(CountryDto))]
        public IActionResult GetCountryOfAnOwner(int ownerId)
        {
            var country = _mapper.Map<CountryDto>
                (_countryRepository.GetCountryByOwner(ownerId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(country);
        }

        // POST method to create a new category
        // -------------------------------------------------------------------------------------------------------------------
        // The CreateCategory method is decorated with the [HttpPost] attribute, indicating that it will handle HTTP POST requests. It also has
        // response type attributes to specify the possible responses from the method.

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCountry([FromBody] CountryDto countryCreate) // The CreateCategory method takes a CountryDto object as a parameter,
                                                                                // which is expected to be provided in the request body.
                                                                                // The [FromBody] attribute indicates that the data should be read
                                                                                // from the request body.
        {
            if (countryCreate == null)
                return BadRequest(ModelState);

            var country = _countryRepository.GetCountries()
                .Where(c => c.Name.Trim().ToUpper() == countryCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (country != null)
            {
                ModelState.AddModelError("", "Country already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var countryMap = _mapper.Map<Country>(countryCreate);

            if (!_countryRepository.CreateCountry(countryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }

            return Ok("Sucessfully Created");
        }
    }
}
