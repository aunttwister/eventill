using MediatR;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommand : IRequest<ReservationDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public int TicketCount { get; set; }
        public long EventOccurrenceId { get; set; }
    }
}
