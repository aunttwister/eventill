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

namespace Reservations.Application.EventOccurrences.Queries.GetEventOccurrenceById
{
    public class GetEventOccurrenceByIdQueryHandler : IRequestHandler<GetEventOccurrenceByIdQuery, EventOccurrenceDto>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetEventOccurrenceByIdQueryHandler(IReservationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<EventOccurrenceDto> Handle(GetEventOccurrenceByIdQuery request, CancellationToken cancellationToken)
        {
            var eventOccurrence = await _dbContext.EventOccurrences.AsNoTracking()
                .FirstOrDefaultAsync(l => l.Id == request.Id && !l.IsDeleted, cancellationToken);
            if (eventOccurrence == null)
            {
                throw new NotFoundException(nameof(Event), request.Id);
            }

            return _mapper.Map<EventOccurrenceDto>(eventOccurrence);
        }
    }
}
