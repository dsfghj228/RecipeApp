using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Recipe
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; } // Количество
        public string Unit { get; set; } // Единица измерения (г, шт и т.д.)

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}