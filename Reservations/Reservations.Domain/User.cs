using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class User : AuditableEntity
    {
        public User()
        {
            Reservations = new HashSet<Reservation>();
        }
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
        public LoginDetails LoginDetails { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
