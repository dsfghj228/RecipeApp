using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;
using api.Models.Recipe;
using Microsoft.EntityFrameworkCore;

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

        public async Task<List<Recipe>> GetRecipes(AppUser user)
        {
            return await _context.Recipes
                            .Where(r => r.AppUserId == user.Id)
                            .Include(r => r.Ingredients)
                            .Include(r => r.Instruction)
                            .ToListAsync();
        }
    }
}