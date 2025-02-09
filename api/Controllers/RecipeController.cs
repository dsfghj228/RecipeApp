using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Models.Dto;
using api.Models.Recipe;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/recipes")]
    [ApiController]
    [Authorize]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeRepository _recipeRepo;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepo, UserManager<AppUser> userManager, IMapper mapper)
        {
            _recipeRepo = recipeRepo;
            _userManager = userManager;
            _mapper = mapper;
        }
        
        [HttpGet]
        public async Task<IActionResult> GetRecipes()
        {
            try {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var appUser = await _userManager.FindByIdAsync(userId);

                var recipes = await _recipeRepo.GetRecipes(appUser);

                if (recipes is null)
                {
                    return NotFound("No recipes found");
                }

                var recipesForReturn = _mapper.Map<List<RecipeForReturn>>(recipes);

                return Ok(recipesForReturn);
            } 
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeModel createRecipeModel)
        {
            if (createRecipeModel == null)
            {
                return BadRequest("Recipe model is null");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser == null)
            {
                return NotFound("User not found");
            }
            
            var recipe = new Recipe
            {
                Name = createRecipeModel.Name,
                Description = createRecipeModel.Description,
                CookTime = createRecipeModel.CookTime,
                Servings = createRecipeModel.Servings,
                Ingredients = _mapper.Map<ICollection<Ingredient>>(createRecipeModel.Ingredients),
                Instruction = _mapper.Map<ICollection<Instruction>>(createRecipeModel.Instruction),
                PhotoUrl = createRecipeModel.PhotoUrl,
                AppUserId = userId,
                AppUser = appUser
            };

            await _recipeRepo.CreateRecipe(recipe);
    
            var recipeForReturn = _mapper.Map<RecipeForReturn>(recipe);

            return CreatedAtAction(nameof(GetRecipes), new { id = recipe.Id }, recipeForReturn);
        }
    }
}