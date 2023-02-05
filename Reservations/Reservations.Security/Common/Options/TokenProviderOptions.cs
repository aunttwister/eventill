using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Common.Options
{
    public class TokenProviderOptions
    {
        public string Path { get; set; } = "/token";

        public string Issuer { get; set; } = "Reservations.Api";

        public string Audience { get; set; } = "reservations-frontend";

        public TimeSpan Expiration { get; set; } = TimeSpan.FromMinutes(240);

        public SigningCredentials SigningCredentials { get; set; }
    }
}
