using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    /// <summary>
    /// Event instance and it's date
    /// </summary>
    public class EventOccurrence : AuditableEntity
    { 
        public EventOccurrence()
        {
            Tickets = new HashSet<Ticket>();
        }
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public long EventId { get; set; }
        public Event Event { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

    }
}
