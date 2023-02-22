using MediatR;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventOccurrences.Commands.EditEventOccurrence
{
    public class EditEventOccurrenceCommand : IRequest
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public long EventId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; }
        public IEnumerable<ReservationDto> Reservations { get; set; }
    }
}
