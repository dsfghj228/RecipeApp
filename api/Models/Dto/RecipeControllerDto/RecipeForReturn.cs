using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.Dto.RecipeDto;
using api.Models.Recipe;

namespace api.Models.Dto
{
    public class RecipeForReturn
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   
        public string Description { get; set; }
        public string CookTime { get; set; }
        public int Servings { get; set; }
        public ICollection<IngredientDto> Ingredients { get; set; }
        public ICollection<InstructionDto> Instruction { get; set; }
        public string PhotoName { get; set; }
    }
}