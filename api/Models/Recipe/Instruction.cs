using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Recipe
{
    public class Instruction
    {
        public int Id { get; set; }
        public string Step { get; set; } // описание шага

        public Guid RecipeId { get; set; }
        public Recipe Recipe { get; set; }
    }
}