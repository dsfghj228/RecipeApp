using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Dto;
using api.Models.Dto.RecipeControllerDto;
using api.Models.Dto.RecipeDto;
using api.Models.Recipe;
using AutoMapper;

namespace api.Helpers
{
    public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateOrUpdateRecipeModel, Recipe>();

        CreateMap<IngredientDto, Ingredient>();
        CreateMap<Ingredient, IngredientDto>();

        CreateMap<InstructionDto, Instruction>();
        CreateMap<Instruction, InstructionDto>();

        CreateMap<Recipe, RecipeForReturn>();
        CreateMap<Recipe, AllRecipeForReturn>();
    }
}

}