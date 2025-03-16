using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models.FavoriteRecipesController;
using api.Models.Recipe;

namespace api.Repository
{
    public class FavoriteRecipeRepository : IFavoriteRecipeRepository
    {
        private readonly ApplicationDBContext _context;

        public FavoriteRecipeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<FavoriteRecipe> AddRecipeToFavorite(Guid recipeId, string userId)
        {
            if (string.IsNullOrWhiteSpace(userId))
            {
                return null;
            }

            var favoriteRecipe = new FavoriteRecipe
            {
                UserId = userId,
                RecipeId = recipeId
            };

            _context.FavoriteRecipes.Add(favoriteRecipe);
            await _context.SaveChangesAsync();

            return favoriteRecipe;
        }
    }
}