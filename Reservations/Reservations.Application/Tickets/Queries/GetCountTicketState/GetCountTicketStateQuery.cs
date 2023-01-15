using MediatR;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Tickets.Queries.GetCountTicketState
{
    public class GetCountTicketStateQuery : IRequest<TicketsStateDto>
    {
        public GetCountTicketStateQuery(string ticketState)
        {
            TicketState = ticketState;
        }

        public string TicketState { get; set; }
        //public long EventOccurrence { get; set; }
    }
}
