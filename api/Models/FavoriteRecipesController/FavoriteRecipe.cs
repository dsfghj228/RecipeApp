using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.FavoriteRecipesController
{
    public class FavoriteRecipe
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string PhotoName { get; set; }
    }
}