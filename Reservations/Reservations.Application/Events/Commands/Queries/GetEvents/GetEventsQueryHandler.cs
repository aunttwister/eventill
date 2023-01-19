using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Commands.Queries.GetEvents
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, List<EventDto>>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEventsQueryHandler(IReservationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<List<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            var eventsQuery = _dbContext.Events.AsNoTracking()
                .Include(e => e.EventType)
                .Include(e => e.Questions)
                .Include(e => e.EventOccurences)
                .ThenInclude(eo => eo.Tickets)
                .Where(e => !e.IsDeleted)
                .OrderByDescending(e => e.Name).AsQueryable();

            var events = await eventsQuery
                .ToListAsync(cancellationToken);
            return _mapper.Map<List<EventDto>>(events);
        }
    }
}
