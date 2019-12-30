using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Infrastructure.Options
{
    public class JwtOptions
    {
        public string IssuerSigningKey { get; set; }
        public string ValidIssuer { get; set; }
        public string ValidAudience { get; set; }
        public int Expires { get; set; }
        public List<User> Users { get; set; }

    }

    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
