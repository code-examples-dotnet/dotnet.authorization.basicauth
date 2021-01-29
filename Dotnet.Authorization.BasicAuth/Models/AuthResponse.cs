using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dotnet.Authorization.BasicAuth.Models
{
    public class AuthResponse
    {
        public bool Authenticated { get; set; }
        public string User { get; set; }
    }
}
