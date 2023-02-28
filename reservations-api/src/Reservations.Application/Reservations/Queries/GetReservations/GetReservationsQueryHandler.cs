using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Queries.GetReservations
{
    public class GetReservationsQueryHandler : IRequestHandler<GetReservationsQuery, List<ReservationDto>>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetReservationsQueryHandler(IReservationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<ReservationDto>> Handle(GetReservationsQuery request, CancellationToken cancellationToken)
        {
            var reservation = await _dbContext.Reservations.AsNoTracking()
                .Include(e => e.User)
                .Include(e => e.Tickets)
                .Where(r => r.Tickets
                    .Any(t => t.TicketState == TicketState.Reserved
                        && !t.IsDeleted) && !r.IsDeleted)
                .ToListAsync(cancellationToken);

            return _mapper.Map<List<ReservationDto>>(reservation);
        }
    }
}
