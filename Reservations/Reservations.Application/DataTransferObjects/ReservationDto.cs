using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.DataTransferObjects
{
    public class ReservationDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<TicketDto> Tickets { get; set; }
        public bool PaymentCompleted { get; set; }
        public long EventOccurrenceId { get; set; }
        public EventOccurrenceDto EventOccurrence { get; set; }
    }
}
