using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Dto.RecipeDto
{
    public class IngredientDto
    {
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Unit { get; set; }
    }
}