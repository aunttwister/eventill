using Reservations.Domain.Enums;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.DataTransferObjects
{
    public class TicketDto
    {
        public long Id { get; set; }
        public TicketState TicketState { get; set; }
        public decimal Price { get; set; }
        public long? ReservationId { get; set; }
        public long EventOccurenceId { get; set; }
        //public ICollection<TicketExtension> TicketExtensions { get; set; }
    }
}
