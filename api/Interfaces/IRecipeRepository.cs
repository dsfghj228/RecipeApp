using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Models.Dto;
using api.Models.Recipe;

namespace api.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> CreateRecipe(Recipe recipe);
        Task<List<Recipe>> GetRecipes(AppUser user);
        Task<List<Recipe>> GetAllRecipesFromDB();
        Task<Recipe> DeleteRecipe(Guid id, string AppUserId);
        Task<Recipe> UpdateRecipe(Guid id, string AppUserId, CreateOrUpdateRecipeModel updateRecipeModel);
    }
}