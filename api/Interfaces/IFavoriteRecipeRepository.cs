using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.FavoriteRecipesController;
using api.Models.Recipe;

namespace api.Interfaces
{
    public interface IFavoriteRecipeRepository
    {
        Task<FavoriteRecipe> AddRecipeToFavorite(Guid recipeId, string userId);
        Task<List<Recipe>> GetFavorites(string userId);
    }
}