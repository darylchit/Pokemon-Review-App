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
            CreateMap<CategoryDto, Category>(); // Maps the CategoryDto to the Category entity, which is used for creating a new category
            CreateMap<Country, CountryDto>(); // Maps the Country entity to the CountryDto
            CreateMap<CountryDto, Country>(); // Maps the CountryDto to the Country entity, which is used for creating a new category
            CreateMap<Owner, OwnerDto>(); // Maps the Owner entity to the OwnerDto
            CreateMap<OwnerDto, Owner>(); // Maps the OwnerDto to the Owner entity, which is used for creating a new category
            CreateMap<Reviewer, ReviewerDto>(); // Maps the Reviewer entity to the ReviewerDto
        }
    }
}
