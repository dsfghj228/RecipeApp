using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Models;
using api.Repository;
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
        private readonly FavoriteRecipeRepository _favRecipeRepo;
        private readonly UserManager<AppUser> _userManager;

        public FavoriteRecipeController(FavoriteRecipeRepository favRecipeRepo, UserManager<AppUser> userManager)
        {
            _favRecipeRepo = favRecipeRepo;
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
        
    }
}