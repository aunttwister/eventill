using MediatR;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Commands.Queries.GetEventsById
{
    public class GetEventsByIdQuery : IRequest<EventDto>
    {
        public long Id { get; set; }
    }
}
