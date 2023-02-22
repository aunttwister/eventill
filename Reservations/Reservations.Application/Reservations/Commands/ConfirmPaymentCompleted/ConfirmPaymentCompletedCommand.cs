using MediatR;
using Reservations.Application.Reservations.Commands.EditReservation;
using Reservations.Application.Tickets.Commands.EditTicket;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Commands.ConfirmPaymentCompleted
{
    public class ConfirmPaymentCompletedCommand : IRequest
    {
        public IEnumerable<EditReservationCommand> Reservations { get; set; }
    }
}
