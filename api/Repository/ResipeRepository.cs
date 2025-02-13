using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using api.Models.Dto;
using api.Models.Recipe;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class ResipeRepository : IRecipeRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public ResipeRepository(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Recipe> CreateRecipe(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();

            return recipe;
        }

        public async Task<Recipe> DeleteRecipe(Guid id, string AppUserId)
        {
            var recipeForDelete = await _context.Recipes
                                        .Include(r => r.Ingredients)
                                        .Include(r => r.Instruction)
                                        .FirstOrDefaultAsync(r => r.Id == id && r.AppUserId == AppUserId);

            if (recipeForDelete is null)
            {
                return null;
            }

            _context.Recipes.Remove(recipeForDelete);
            await _context.SaveChangesAsync();

            return recipeForDelete;
        }

        public async Task<List<Recipe>> GetAllRecipesFromDB(QueryObject query)
        {
            var recipes = _context.Recipes
                                    .Include(r => r.Ingredients)
                                    .Include(r => r.Instruction)
                                    .AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                recipes = recipes.Where(r => r.Name.Contains(query.Name));
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await recipes.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }

        public async Task<Recipe> GetRecipeById(Guid id)
        {
            return await _context.Recipes
                            .Include(r => r.Ingredients)
                            .Include(r => r.Instruction)
                            .Include(r => r.AppUser)
                            .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<List<Recipe>> GetRecipes(AppUser user)
        {
            return await _context.Recipes
                            .Where(r => r.AppUserId == user.Id)
                            .Include(r => r.Ingredients)
                            .Include(r => r.Instruction)
                            .ToListAsync();
        }

        public async Task<Recipe> UpdateRecipe(Guid id, string AppUserId, CreateOrUpdateRecipeModel updateRecipeModel)
        {
            var recipeForUpdate = await _context.Recipes.FirstOrDefaultAsync(r => r.Id == id && r.AppUserId == AppUserId);

            if (recipeForUpdate is null)
            {
                return null;
            }

            recipeForUpdate.Name = updateRecipeModel.Name;
            recipeForUpdate.Description = updateRecipeModel.Description;
            recipeForUpdate.CookTime = updateRecipeModel.CookTime;
            recipeForUpdate.Servings = updateRecipeModel.Servings;
            recipeForUpdate.Ingredients = _mapper.Map<ICollection<Ingredient>>(updateRecipeModel.Ingredients);
            recipeForUpdate.Instruction = _mapper.Map<ICollection<Instruction>>(updateRecipeModel.Instruction);
            recipeForUpdate.PhotoName = updateRecipeModel.PhotoName;

            _context.Recipes.Update(recipeForUpdate);
            await _context.SaveChangesAsync();

            return recipeForUpdate;
        }
    }
}