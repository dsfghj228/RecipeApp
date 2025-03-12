using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.Models.FavoriteRecipesController;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        public string PhotoName { get; set; } = "";
        
        [JsonIgnore]
        public ICollection<api.Models.Recipe.Recipe> Recipes { get; set; }

        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    }
}