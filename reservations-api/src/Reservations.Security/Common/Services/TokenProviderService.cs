using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
        public object GenerateToken(string userEmail, string roleName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userEmail),
                new Claim(ClaimTypes.Role, roleName)
            };

            byte[] tokenKey = Convert.FromBase64String(_options.Secret);

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audiences.First(),
                claims: claims,
                expires: DateTime.UtcNow.Add(_options.Expiration),
                signingCredentials: _options.SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256));

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

        public ClaimsPrincipal ExtractClaims(string encodedString)
        {
            var token = new JwtSecurityToken(encodedString);
            byte[] tokenKey = Convert.FromBase64String(_options.Secret);

            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters()
            {
                ValidateLifetime = false,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                ValidAudiences = _options.Audiences,
                ValidIssuer = _options.Issuer,
                IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                ClockSkew = TimeSpan.Zero
            };

            SecurityToken validatedToken;
            return new JwtSecurityTokenHandler().ValidateToken(encodedString, tokenValidationParameters, out validatedToken);
        }
    }
}
