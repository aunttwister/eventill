using MediatR;
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

    public class EditReservationCommand : IRequest
    {
        public long Id { get; set; }
        public bool PaymentCompleted { get; set; }
    }
}
