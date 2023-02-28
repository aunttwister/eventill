using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class EventQuestion : AuditableEntity
    {
        public long Id { get; set; }
        public long EventId { get; set; }
        public long QuestionId { get; set; }
        public Event Event { get; set; }
        public Question Question { get; set; }
    }
}
