using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Queries.GetEventById
{
    public class GetEventsByIdQueryHandler : IRequestHandler<GetEventsByIdQuery, EventDto>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEventsByIdQueryHandler(IReservationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<EventDto> Handle(GetEventsByIdQuery request, CancellationToken cancellationToken)
        {
            var eventInstace = await _dbContext.Events.AsNoTracking()
                .Include(e => e.EventType)
                .Include(e => e.Questions)
                .Include(e => e.EventOccurrences)
                .ThenInclude(eo => eo.Tickets)
                .FirstOrDefaultAsync(l => l.Id == request.Id && !l.IsDeleted, cancellationToken);
            if (eventInstace == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            return _mapper.Map<EventDto>(eventInstace);
        }
    }
}
