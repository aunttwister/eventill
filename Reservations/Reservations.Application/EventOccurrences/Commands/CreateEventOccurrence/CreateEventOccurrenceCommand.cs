using MediatR;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventOccurrences.Commands.CreateEventOccurrence
{
    public class CreateEventOccurrenceCommand : IRequest<EventOccurrenceDto>
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public long? EventId { get; set; }
        public Event Event { get; set; }
    }
}
