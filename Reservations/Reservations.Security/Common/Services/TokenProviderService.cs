using Microsoft.Extensions.Options;
using Reservations.Domain;
using Reservations.Security.Common.Interfaces;
using Reservations.Security.Common.Options;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Common.Services
{
    public class TokenProviderService : ITokenProviderService
    {
        private readonly TokenProviderOptions _options;
        public TokenProviderService(IOptions<TokenProviderOptions> options)
        {
            _options = options.Value;
        }
        public object GenerateToken(Guid userId, string roleName)
        {
            DateTime now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId.ToString())
            };
            claims.Add(new Claim(ClaimTypes.Role, roleName));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);

            string token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new 
            { 
                access_token = token, 
                expires_in = (int)_options.Expiration.TotalSeconds 
            };
        }

        public string GenerateRefreshToken(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
