using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Common.Interfaces
{
    public interface ITokenProviderService
    {
        public string GenerateRefreshToken(string userId);
        public string GenerateToken(User user);
    }
}
