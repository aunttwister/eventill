using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Common.Options
{
    public class AuthenticationServiceOptions
    {
        public int SaltSize { get; set; } = 16;
        public int HashSize { get; set; } = 20;
        public int HashIter { get; set; } = 10000;
    }
}
