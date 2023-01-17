using AutoMapper;
using MediatR;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.EventOccurrences.Commands.CreateEventOccurrence;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventTypes.Commands.CreateEventType
{
    public class CreateEventTypeCommandHandler : IRequestHandler<CreateEventTypeCommand, EventTypeDto>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        public CreateEventTypeCommandHandler(
            IMapper mapper,
            IReservationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<EventTypeDto> Handle(CreateEventTypeCommand request, CancellationToken cancellationToken)
        {
            EventType newEventType = _mapper.Map<EventType>(request);

            if (await _dbContext.EventTypes.EventTypeExistsAsync(newEventType, cancellationToken))
                throw new AlreadyExistsException($"{nameof(EventType)} with the same name already exists.");

            _dbContext.EventTypes.Add(newEventType);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventTypeDto>(newEventType);
        }
    }
}
