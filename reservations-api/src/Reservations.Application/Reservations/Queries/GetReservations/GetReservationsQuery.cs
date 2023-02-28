using MediatR;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Queries.GetReservations
{
    public class GetReservationsQuery : IRequest<List<ReservationDto>>
    {
        public long EventOccurrenceId { get; set; }
    }
}
