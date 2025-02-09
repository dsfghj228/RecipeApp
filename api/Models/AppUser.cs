using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace api.Models
{
    public class AppUser : IdentityUser
    {
        [JsonIgnore]
        public ICollection<api.Models.Recipe.Recipe> Recipes { get; set; }
    }
}