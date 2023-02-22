using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Helpers;
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
    public class GetEventByIdQueryHandler : IRequestHandler<GetEventByIdQuery, EventDto>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEventByIdQueryHandler(IReservationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<EventDto> Handle(GetEventByIdQuery request, CancellationToken cancellationToken)
        {
            var eventInstace = await _dbContext.Events
                .Include(e => e.EventType)
                .Include(e => e.Questions)
                .Include(e => e.EventOccurrences)
                .Include(e => e.EventOccurrences).ThenInclude(eo => eo.Reservations).ThenInclude(r => r.User)
                .Include(e => e.EventOccurrences).ThenInclude(eo => eo.Tickets).AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == request.Id, cancellationToken);
            if (eventInstace == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            return _mapper.Map<EventDto>(eventInstace);
        }
    }
}
