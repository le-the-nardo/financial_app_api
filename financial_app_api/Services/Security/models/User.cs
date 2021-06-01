using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financial_app_api.Services.Security.models
{
    public class AppUser
    {
        public int user_id {get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public string cpf { get; set; }
        public string email { get; set; }
    }
}

