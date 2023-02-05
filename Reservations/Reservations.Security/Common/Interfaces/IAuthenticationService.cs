using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Common.Interfaces
{
    public interface IAuthenticationService
    {
        public (byte[] hashPassword, byte[] salt) GenerateHash(string password);
        public bool Verify(string password, byte[] salt, byte[] storedHash);
    }
}
