using MediatR;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Commands.EditReservation
{
    public class EditReservationCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IEnumerable<Ticket> Tickets { get; set; }
        public bool PaymentCompleted { get; set; }
        public long EventOccurrenceId { get; set; }
        public bool IsDeleted { get; set; }
        public Guid UserId { get; set; }
    }
}
