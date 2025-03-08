using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models.Dto
{
    public class NewUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }
        public string PhotoName { get; set; }
    }
}