using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api.Models.FavoriteRecipesController
{
    public class FavoriteRecipe
    {
        public string UserId { get; set; }

        [JsonIgnore]
        public AppUser User { get; set; }

        public Guid RecipeId { get; set; }

        [JsonIgnore]
        public api.Models.Recipe.Recipe Recipe { get; set; }
    }
}