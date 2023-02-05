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
        private TokenProviderOptions _options;
        public TokenProviderService(IOptions<TokenProviderOptions> options)
        {
            _options = options.Value;
        }
        public string GenerateToken(User user)
        {
            DateTime now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
            claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        public string GenerateRefreshToken(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
