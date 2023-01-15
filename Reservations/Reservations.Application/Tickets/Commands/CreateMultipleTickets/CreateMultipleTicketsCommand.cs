using MediatR;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Tickets.Commands.CreateMultipleTickets
{
    public class CreateMultipleTicketsCommand : IRequest<IEnumerable<TicketDto>>
    {
        public int TicketCount { get; set; }
        public decimal Price { get; set; }
        public EventOccurrence EventOccurrence { get; set; }
    }
}
