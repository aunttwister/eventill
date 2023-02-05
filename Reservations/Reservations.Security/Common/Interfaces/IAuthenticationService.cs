using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Common.Interfaces
{
    public interface IAuthenticationService
    {
        public byte[] HashPassword(string password, byte[] salt);
        public byte[] ToArray();
        public bool Verify(string password);
    }
}
