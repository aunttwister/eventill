using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.DataTransferObjects
{
    public class LoginDetailsDto
    {
        public long Id { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] Salt { get; set; }
        public Guid UserId { get; set; }
    }
}
