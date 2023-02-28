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
        public DateTime StartTime { get; set; }
        public long EventId { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; }
        public IEnumerable<ReservationDto> Reservations { get; set; }
        public uint TotalTicketCount { get; set; }
        public uint AvailableTicketCount { get; set; }
        public uint ReservedTicketCount { get; set; }
        public uint SoldTicketCount { get; set; }
        public bool IsActive { get; set; }
    }
}
