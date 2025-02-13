using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Models.Dto;
using api.Models.Dto.RecipeControllerDto;
using api.Models.Dto.RecipeDto;
using api.Models.Recipe;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/recipes")]
    [ApiController]
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
        [Authorize]
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

        [HttpGet("/api/allRecipes")]
        public async Task<IActionResult> GetAllRecipesFromDb()
        {
            var recipes = await _recipeRepo.GetAllRecipesFromDB();

            if (recipes is null)
            {
                return NotFound("No recipes found");
            }

            var recipesForReturn = _mapper.Map<List<RecipeForReturn>>(recipes);

            return Ok(recipesForReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecipeById(Guid id)
        {
            var recipe = await _recipeRepo.GetRecipeById(id);

            if (recipe is null)
            {
                return NotFound("Recipe not found");
            }

            var recipeForReturn = new GetByIdRecipeForReturn 
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                CookTime = recipe.CookTime,
                Servings = recipe.Servings,
                Ingredients = _mapper.Map<ICollection<IngredientDto>>(recipe.Ingredients),
                Instruction = _mapper.Map<ICollection<InstructionDto>>(recipe.Instruction),
                PhotoName = recipe.PhotoName,
                UserName = recipe.AppUser.UserName
            };

            return Ok(recipeForReturn);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateOrUpdateRecipeModel createRecipeModel)
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
                PhotoName = createRecipeModel.PhotoName,
                AppUserId = userId,
                AppUser = appUser
            };

            await _recipeRepo.CreateRecipe(recipe);
    
            var recipeForReturn = _mapper.Map<RecipeForReturn>(recipe);

            return CreatedAtAction(nameof(GetRecipes), new { id = recipe.Id }, recipeForReturn);
        }

        [HttpDelete("id")]
        [Authorize]
        public async Task<IActionResult> DeleteRecipe(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            var recipeForDelete = await _recipeRepo.DeleteRecipe(id, userId);

            if (recipeForDelete is null)
            {
                return NotFound("User id is invalid or such recipe doesn't exist");
            }

            var recipeForReturn = _mapper.Map<RecipeForReturn>(recipeForDelete);

            return Ok(recipeForReturn);
        }

        [HttpPut("id")]
        [Authorize]
        public async Task<IActionResult> UpdateRecipe(Guid id, CreateOrUpdateRecipeModel updateRecipeModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var recipeForUpdate = await _recipeRepo.UpdateRecipe(id, userId, updateRecipeModel);

            if (recipeForUpdate is null)
            {
                return NotFound("User id is invalid or such recipe doesn't exist");
            }

            var recipeForReturn = _mapper.Map<RecipeForReturn>(recipeForUpdate);

            return Ok(recipeForReturn);
        }
    }
}