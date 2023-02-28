using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Interfaces
{
    public interface ITicketService
    {
        void ResetTicketState(List<Ticket> tickets, TicketState ticketState);
    }
}
