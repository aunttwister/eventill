using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class EventType : AuditableEntity
    {
        public EventType()
        {
            Events = new HashSet<Event>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
