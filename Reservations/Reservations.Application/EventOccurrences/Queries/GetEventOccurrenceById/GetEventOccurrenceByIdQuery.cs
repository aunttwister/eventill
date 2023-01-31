using MediatR;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventOccurrences.Queries.GetEventOccurrenceById
{
    public class GetEventOccurrenceByIdQuery : IRequest<EventOccurrenceDto>
    {
        public long Id { get; set; }
    }
}
