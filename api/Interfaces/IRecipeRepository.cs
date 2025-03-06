using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Helpers;
using api.Models;
using api.Models.Dto;
using api.Models.Recipe;

namespace api.Interfaces
{
    public interface IRecipeRepository
    {
        Task<Recipe> CreateRecipe(Recipe recipe);
        Task<List<Recipe>> GetRecipes(AppUser user);
        Task<List<Recipe>> GetAllRecipesFromDB(QueryObject query);
        Task<Recipe> GetRecipeById(Guid id);
        Task<int> GetRecipeCount();
        Task<Recipe> DeleteRecipe(Guid id, string AppUserId);
        Task<Recipe> UpdateRecipe(Guid id, string AppUserId, CreateOrUpdateRecipeModel updateRecipeModel);
    }
}