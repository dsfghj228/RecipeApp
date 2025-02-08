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

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] CreateRecipeModel createRecipeModel,IFormFile photo)
        {
            if (createRecipeModel is null)
            {
                return BadRequest("Recipe model is null");
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var appUser = await _userManager.FindByIdAsync(userId);

            if (appUser is null)
            {
                return NotFound("User not found");
            }

            if (photo != null)
            {
                var filePath = Path.Combine("wwwroot/images", Guid.NewGuid() + Path.GetExtension(photo.FileName));
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                var recipe = new Recipe
                {
                    Name = createRecipeModel.Name,
                    Description = createRecipeModel.Description,
                    CookTime = createRecipeModel.CookTime,
                    Servings = createRecipeModel.Servings,
                    Ingredients = _mapper.Map<Ingredient[]>(createRecipeModel.Ingredients),
                    Instruction = _mapper.Map<Instruction[]>(createRecipeModel.Instruction),
                    PhotoUrl = "/images/" + Path.GetFileName(filePath),
                    AppUserId = userId,
                    AppUser = appUser
                };

                await _recipeRepo.CreateRecipe(recipe);
                
                var recipeForReturn = _mapper.Map<RecipeForReturn>(recipe);

                return CreatedAtAction("Well done", new {id = recipe.Id}, recipeForReturn);
            }
            else
            {
                return BadRequest("Photo is required");
            }
        }
    }
}