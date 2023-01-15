using AutoMapper;
using MediatR;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventOccurrences.Commands.CreateEventOccurrence
{
    public class CreateEventOccurrenceCommandHandler : IRequestHandler<CreateEventOccurrenceCommand, EventOccurrenceDto>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        public CreateEventOccurrenceCommandHandler(
            IMapper mapper,
            IReservationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<EventOccurrenceDto> Handle(CreateEventOccurrenceCommand request, CancellationToken cancellationToken)
        {
            EventOccurrence newEventOccurrence = _mapper.Map<EventOccurrence>(request);

            if (await _dbContext.EventOccurrences.EventOccurrenceExistsAsync(newEventOccurrence, cancellationToken))
                throw new AlreadyExistsException($"{nameof(EventOccurrence)}/s with stated date already exist.");

            _dbContext.EventOccurrences.Add(newEventOccurrence);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventOccurrenceDto>(newEventOccurrence);
        }
    }
}
