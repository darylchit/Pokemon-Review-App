using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Interfaces;
using PokemonReviewApp.Models;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace PokemonReviewApp.Controllers // 7. Creating the Pokemon Controller and Implementing the GetPokemons Endpoint
{
    [Route("api/[controller]")] // This attribute defines the route for the controller.
                                // The [controller] token will be replaced with the name of the controller,
                                // which is "Pokemon" in this case. So the route will be "api/pokemon".
                                // Tells Web API that this is a controller and to use the route "api/pokemon" for this controller.
    [ApiController]
    public class PokemonController : ControllerBase // must inherit from ControllerBase to be a controller, which provides the necessary
                                                    // functionality for handling
                                                    // HTTP requests and responses.

                                                    // a field is a variable that is declared at the class level and can be accessed by all methods
                                                    // within the class.
                                                    // A readonly field is a field that can only be assigned a value during its declaration or in
                                                    // the constructor of the class.
                                                    // Once assigned, its value cannot be changed.
    {
        private readonly IPokemonRepository _pokemonRepository; // 1. The controller has a private readonly field called _pokemonRepository of
        private readonly IMapper _mapper;                       // type IPokemonRepository.
                                                                // This field will be used to access the data related to Pokemon.
        public PokemonController(IPokemonRepository pokemonRepository, IMapper mapper)  // 2. The constructor of the PokemonController class takes an
                                                                        // IPokemonRepository object as a parameter and assigns it to the
                                                                        // _pokemonRepository field.
        {
            _pokemonRepository = pokemonRepository; // 3. The constructor initializes the _pokemonRepository field with the
            _mapper = mapper;                                        // provided IPokemonRepository object,
                                                    // allowing the controller to use it for data access related to Pokemon.
        }

        [HttpGet] // 4. The GetPokemons method is decorated with the [HttpGet] attribute, indicating that it will handle HTTP GET requests.
        [ProducesResponseType(200, Type = typeof(IEnumerable<PokemonDto>))] // 5. The [ProducesResponseType] attribute specifies the expected
                                                                         // HTTP status code and the type of data that will be returned by
                                                                         // the GetPokemons method.
        public IActionResult GetPokemons() // 6. The GetPokemons method is a public method that returns an IActionResult.
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(_pokemonRepository.GetPokemons()); // 7. Inside the GetPokemons method, it retrieves a list of Pokemon from
                                                                                            // the _pokemonRepository.
                                                                                            // Not hard coded, we use the repository to get the data, which allows for better separation
                                                                                            // of concerns and easier testing.
                                                                                            // The repository is responsible for data access, while the controller is responsible for
                                                                                            // handling HTTP requests and responses.

            // AutoMapper is used to map the list of Pokemon entities to a list of PokemonDto objects,
            // which will be returned to the client.
            // We used Mapper because the API was not showing all the properties, only the Id and Name, and we want to show all the properties of the Pokemon entity,
            // so we created a DTO (Data Transfer Object) for the Pokemon entity, which will be used to transfer data between the client and the server.


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons); // 8. It checks if the model state is valid. If it is not valid, it returns a BadRequest response with the model state
                                 // errors.

            // Video 6 GET & Read Methods
        } // Next, we want to create a DTO (Data Transfer Object) for the Pokemon entity, which will be used to transfer data between the
          // client and the server.
          // A DTO is a simple object that contains only the data that needs to be transferred, without any business logic or behavior.

        // We will create a PokemonDto class that will contain the properties that we want to expose to the client, in PokemonDto.cs 

        // This helps to decouple the internal representation of the data from the external representation, and allows for better control
        // over what data is exposed to the client.
        // DTO is used to make sure we are not returning more data than we need to the client, which can help to
        // improve performance and reduce bandwidth usage and also to avoid exposing sensitive data to the client.
        // A DTO makes sure that people can't send data that we don't want them to send,
        // and it also makes sure that we are not sending data that we don't want to send.

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(PokemonDto))] // 1. The GetPokemon method is decorated with the [HttpGet("{pokeId}")] attribute,
                                                            // indicating that it
                                                            // will handle HTTP GET requests with a route parameter called "pokeId".
        [ProducesResponseType(400)]
        public IActionResult GetPokemon(int pokeId) // 2. The GetPokemon method is a public method that takes an integer parameter
                                            // called "id" and returns an IActionResult.
                                                // IActionResult is a return type that allows us to return different types of HTTP
                                                // responses, such as Ok, BadRequest, NotFound, etc.
                                                // We have to return IActionResult because we used it earlier in the
                                                // GetPokemons method, and we want to be consistent with our return types.
         
        {
            if (!_pokemonRepository.PokemonExists(pokeId)) // Validations
                return NotFound();

            var pokemon = _mapper.Map<PokemonDto>(_pokemonRepository.GetPokemon(pokeId)); //Inside the GetPokemon method, it first
                                                                                       //checks if a Pokemon with the specified
                                                                                       //"pokeId" exists in the repository.
                                                                                       //If it does not exist, it returns a NotFound response.
            // Map Pokemon to Dto
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemon);
        }

        [HttpGet("{pokeId}/rating")] // Get Rating
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonRating(int pokeId)
        {
            if (!_pokemonRepository.PokemonExists(pokeId))
                return NotFound();

            var rating = _pokemonRepository.GetPokemonRating(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(); // ModelState allows us to check if the data sent by the client is valid according
                                     // to the model's validation rules. If the model state is not valid, we return a BadRequest
                                     // response to indicate that the request was not properly formed.
                                     // for example, if something is required and the client did not send it,
                                     // or if the data type is incorrect, the model state will be invalid.
            return Ok(rating);
        }

    }
}
