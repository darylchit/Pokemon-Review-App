using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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
                                                                // type IPokemonRepository.
                                                                // This field will be used to access the data related to Pokemon.
        public PokemonController(IPokemonRepository pokemonRepository)  // 2. The constructor of the PokemonController class takes an
                                                                        // IPokemonRepository object as a parameter and assigns it to the
                                                                        // _pokemonRepository field.
        {
            _pokemonRepository = pokemonRepository; // 3. The constructor initializes the _pokemonRepository field with the
                                                    // provided IPokemonRepository object,
                                                    // allowing the controller to use it for data access related to Pokemon.
        }

        [HttpGet] // 4. The GetPokemons method is decorated with the [HttpGet] attribute, indicating that it will handle HTTP GET requests.
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))] // 5. The [ProducesResponseType] attribute specifies the expected
                                                                         // HTTP status code and the type of data that will be returned by
                                                                         // the GetPokemons method.
        public IActionResult GetPokemons() // 6. The GetPokemons method is a public method that returns an IActionResult.
        {
            var pokemons = _pokemonRepository.GetPokemons(); // 7. Inside the GetPokemons method, it retrieves a list of Pokemon from
                                                             // the _pokemonRepository.
                                                             // Not hard coded, we use the repository to get the data, which allows for better separation
                                                             // of concerns and easier testing.
                                                             // The repository is responsible for data access, while the controller is responsible for
                                                             // handling HTTP requests and responses.
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(pokemons); // 8. It checks if the model state is valid. If it is not valid, it returns a BadRequest response with the model state
                                 // errors.
        } 
    }
}
