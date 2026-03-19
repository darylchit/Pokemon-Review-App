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
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository; // Looks in ICategoryRepository.cs for definition
        private readonly IMapper _mapper;
        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper) // Inject Repository and AutoMapper into controller
        {
            _categoryRepository = categoryRepository; // This is our whole interface which is inserted above ^^^
            _mapper = mapper;
        }

        [HttpGet] // 4. The GetPokemons method is decorated with the [HttpGet] attribute, indicating that it will handle HTTP GET requests.
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryDto>))]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(categories); // It checks if the model state is valid. If it is not valid, it returns a BadRequest response with the model state
                                   // errors.
        }
        [HttpGet("{categoryId}")]
        [ProducesResponseType(200, Type = typeof(CategoryDto))]
        [ProducesResponseType(400)]

        public IActionResult GetCategory(int categoryId)
        {
            if (!_categoryRepository.CategoryExists(categoryId)) // Validations
                return NotFound();

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(category);
        }

        [HttpGet("pokemon/{categoryId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Pokemon>))]
        [ProducesResponseType(400)]
        public IActionResult GetPokemonByCategoryId(int categoryId)
        {
            var pokemons = _mapper.Map<List<PokemonDto>>(
                _categoryRepository.GetPokemonByCategory(categoryId));

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(pokemons); // Now we go to MappingProfile.cs in Helper to make sure we have a map for PokemonDto and CategoryDto
        }



        // POST method to create a new category
        // -------------------------------------------------------------------------------------------------------------------------------
        // The CreateCategory method is decorated with the [HttpPost] attribute, indicating that
        // it will handle HTTP POST requests. It takes a CategoryDto object as input from the request body.

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
                return BadRequest(ModelState);

            var category = _categoryRepository.GetCategories()
                .Where(c => c.Name.Trim().ToUpper() == categoryCreate.Name.TrimEnd().ToUpper()) // checking to see if instance already exists
                .FirstOrDefault();

            if(category != null)
            {
                ModelState.AddModelError("", "Category already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState); // checking if the model state is valid. If it is not valid,
                                               // it returns a BadRequest response with the model state errors.

            var categoryMap = _mapper.Map<Category>(categoryCreate); // If the model state is valid, it maps the CategoryDto
                                                                     // to a Category entity using AutoMapper. This is done to convert the
                                                                     // DTO into a format that can be saved to the database.

            if (!_categoryRepository.CreateCategory(categoryMap)) // It then calls the CreateCategory method of the _categoryRepository
                                                                  // to save the new category to the database. If the creation fails,
            {
                ModelState.AddModelError("", "Something went wrong while saving"); // it adds a model error to the ModelState and
                                                                                   // returns a 500 Internal Server Error
                                                                                   // response with the model state errors.
                return StatusCode(500, ModelState);
            }

            return Ok("Sucessfully Created");
        }
    }
}
