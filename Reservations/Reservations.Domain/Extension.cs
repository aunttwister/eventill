using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class Extension : AuditableEntity
    {
        public Extension()
        {
            TicketExtensions = new HashSet<TicketExtension>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public ICollection<TicketExtension> TicketExtensions { get; set; }
    }
}
