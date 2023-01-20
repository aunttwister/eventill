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
        public GetCountTicketStateQuery(long eventOccurrenceId, string ticketState)
        {
            EventOccurrenceId = eventOccurrenceId;
            TicketState = ticketState;
        }

        public long EventOccurrenceId { get; set; }
        public string TicketState { get; set; }
    }
}
