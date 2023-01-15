using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class Ticket : AuditableEntity
    {
        public Ticket()
        {
            TicketExtensions = new HashSet<TicketExtension>();
        }
        public long Id { get; set; }
        public TicketState TicketState { get; set; }
        public decimal Price { get; set; }
        public long? ReservationId { get; set; }
        public Reservation Reservation { get; set; }
        public long? EventOccurenceId { get; set; }
        public EventOccurrence EventOccurence { get; set; }
        public ICollection<TicketExtension> TicketExtensions { get; set; }
    }
}
