using AutoMapper;
using PokemonReviewApp.Dto;
using PokemonReviewApp.Models;

namespace PokemonReviewApp.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Pokemon, PokemonDto>(); // Maps the Pokemon entity to the PokemonDto
            CreateMap<Category, CategoryDto>(); // Maps the Category entity to the CategoryDto
            CreateMap<Country, CountryDto>(); // Maps the Country entity to the CountryDto
        }
    }
}
