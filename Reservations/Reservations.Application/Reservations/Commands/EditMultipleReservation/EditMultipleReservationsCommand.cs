using MediatR;
using Reservations.Application.Reservations.Commands.ConfirmPaymentCompleted;
using Reservations.Application.Reservations.Commands.EditReservation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Commands.EditMultipleReservation
{
    public class EditMultipleReservationsCommand : IRequest
    {
        public IEnumerable<EditReservationCommand> Reservations { get; set; }
    }
}
