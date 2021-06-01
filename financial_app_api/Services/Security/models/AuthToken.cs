using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financial_app_api.Security.models
{
    public class AuthToken
    {
        public string session_id { get; set; }
        public bool authenticated { get; set; } = false;
        public string role { get; set; }
        public string name { get; set; }
        public DateTime requestAt { get; set; }
        public DateTime created { get; set; }
        public DateTime expiration { get; set; }
        public string accessToken { get; set; }
        public string refreshToken { get; set; }
        public double expiresIn { get; set; }
        public string tokenType { get; set; } = "Bearer";
        public string message { get; set; }
    }
}
