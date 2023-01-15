using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class TicketExtension : AuditableEntity
    {
        public long Id { get; set; }
        public long TicketId { get; set; }
        public Ticket Ticket { get; set; }
        public long ExtensionId { get; set; }
        public Extension Extension { get; set; }
    }
}
