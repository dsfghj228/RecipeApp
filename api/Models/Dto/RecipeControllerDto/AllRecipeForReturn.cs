using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Dto.RecipeControllerDto
{
    public class AllRecipeForReturn
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   
        public string Description { get; set; }
        public string PhotoName { get; set; }
    }
}