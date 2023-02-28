using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Domain
{
    public class Question : AuditableEntity
    {
        public Question()
        {
            Events = new HashSet<Event>();
        }
        public long Id { get; set; }
        public string Content { get; set; }
        public ICollection<Event> Events { get; set; }
    }
}
