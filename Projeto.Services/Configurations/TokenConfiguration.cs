using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projeto.Services.Configurations
{
    public class TokenConfiguration
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Seconds { get; set; }

    }
}
