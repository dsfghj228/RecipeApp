using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models.Recipe;

namespace api.Repository
{
    public class ResipeRepository : IRecipeRepository
    {
        private readonly ApplicationDBContext _context;

        public ResipeRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }
    }
}