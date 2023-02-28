using Reservations.Application.DataTransferObjects;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Tickets.Commands.EditTicket
{
    public class EditTicketCommand
    {
        public long Id { get; set; }
        public TicketState TicketState { get; set; }
        public decimal Price { get; set; }
        public long? ReservationId { get; set; }
        public ReservationDto Reservation { get; set; }
        public long EventOccurenceId { get; set; }
    }
}
