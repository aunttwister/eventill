using Microsoft.Extensions.Options;
using Reservations.Security.Common.Interfaces;
using Reservations.Security.Common.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Common.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly AuthenticationServiceOptions _options;
        public AuthenticationService(IOptions<AuthenticationServiceOptions> options)
        {
            _options = options.Value;
        }
        public (byte[] hashPassword, byte[] salt) GenerateHash(string password)
        {
            using (var rng = RandomNumberGenerator.Create())
            {
                var salt = new byte[_options.SaltSize];
                rng.GetBytes(salt);
                var hash = HashPassword(password, salt);

                return (hash, salt);
            }
        }
        public bool Verify(string password, byte[] salt, byte[] storedHash)
        {
            byte[] inputHash = HashPassword(password, salt);

            return CompareHash(inputHash, storedHash);
        }
        private byte[] HashPassword(string password, byte[] salt)
        {
            return new Rfc2898DeriveBytes(password, salt, _options.HashIter).GetBytes(_options.HashSize);
        }
        private bool CompareHash(byte[] inputHash, byte[] storedHash)
        {
            for (int i = 0; i < _options.HashSize; i++)
                if (inputHash[i] != storedHash[i])
                    return false;
            return true;
        }
    }
}
