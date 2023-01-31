using MediatR;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Queries.GetEventById
{
    public class GetEventByIdQuery : IRequest<EventDto>
    {
        public long Id { get; set; }
    }
}
