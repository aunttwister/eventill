using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class LoginDetails : AuditableEntity
    {
        public long Id { get; set; }
        public byte[] HashPassword { get; set; }
        public byte[] Salt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
