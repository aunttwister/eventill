using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class Event : AuditableEntity
    {
        public Event()
        {
            Questions = new HashSet<Question>();
            EventOccurences = new HashSet<EventOccurrence>();
        }
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //Stored as ticks
        public long Length { get; set; }
        public int EventTypeId { get; set; }
        public EventType EventType { get; set; }
        public ICollection<Question> Questions { get; set; }
        public ICollection<EventOccurrence> EventOccurences { get; set; }
    }
}
