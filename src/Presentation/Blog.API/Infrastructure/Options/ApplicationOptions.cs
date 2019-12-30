using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.API.Infrastructure.Options
{
    public class ApplicationOptions
    {
        public ConnectionStringsOptions ConnectionStrings { get; set; }

        public JwtOptions Jwt { get; set; }
    }
}
