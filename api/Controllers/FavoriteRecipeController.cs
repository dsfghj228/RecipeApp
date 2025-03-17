using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Models.Dto;
using api.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("api/favorites")]
    [ApiController]
    [Authorize]
    public class FavoriteRecipeController : ControllerBase
    {
        private readonly IFavoriteRecipeRepository _favRecipeRepo;
        private readonly IMapper _mapper;

        public FavoriteRecipeController(IFavoriteRecipeRepository favRecipeRepo, IMapper mapper)
        {
            _favRecipeRepo = favRecipeRepo;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddToFavorites([FromBody] Guid recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized("User is unauthorized");
            }

            var favRecipe = await _favRecipeRepo.AddRecipeToFavorite(recipeId, userId);

            if(favRecipe is null)
            {
                return BadRequest("UserId or/and recipeId is wrong");
            }

            return Ok(favRecipe);
        }

        [HttpGet]
        public async Task<IActionResult> GetFavarites()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized("User is unathorized");
            }

            var favRecipes = await _favRecipeRepo.GetFavorites(userId);

            if(favRecipes == null)
            {
                return BadRequest("User not found");
            }

            var recipesForReturn = _mapper.Map<List<RecipeForReturn>>(favRecipes);

            return Ok(recipesForReturn);
        }

        [HttpDelete("recipeId")]
        public async Task<IActionResult> RemoveFromFavorites([FromQuery] Guid recipeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                return Unauthorized("User is unathorized");
            }

            var recipe = await _favRecipeRepo.RemoveFromFavorites(recipeId, userId);

            if (recipe == null)
            {
                return BadRequest("User or/and recipe not found");
            }

            return Ok(recipe);
        }

        
    }
}