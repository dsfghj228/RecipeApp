using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.FavoriteRecipesController;

namespace api.Models.Recipe
{
    public class Recipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   
        public string Description { get; set; }
        public string CookTime { get; set; }
        public int Servings { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Instruction> Instruction { get; set; }
        public string PhotoName { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public ICollection<FavoriteRecipe> FavoriteRecipes { get; set; }
    }
}