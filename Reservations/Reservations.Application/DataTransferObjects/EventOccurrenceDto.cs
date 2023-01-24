using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.DataTransferObjects
{
    public class EventOccurrenceDto
    {
        public long Id { get; set; }
        public string StartTime { get; set; }
        public long EventId { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; }
    }
}
