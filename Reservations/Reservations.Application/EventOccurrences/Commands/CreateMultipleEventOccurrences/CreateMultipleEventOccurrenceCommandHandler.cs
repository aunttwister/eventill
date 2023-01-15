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

namespace Reservations.Application.EventOccurrences.Commands.CreateMultipleEventOccurrences
{
    public class CreateMultipleEventOccurrenceCommandHandler : IRequestHandler<CreateMultipleEventOccurrenceCommand, IEnumerable<EventOccurrenceDto>>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        public CreateMultipleEventOccurrenceCommandHandler(
            IMapper mapper,
            IReservationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EventOccurrenceDto>> Handle(CreateMultipleEventOccurrenceCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<EventOccurrence> eventOccurrences = _mapper.Map<IEnumerable<EventOccurrence>>(request.EventOccurrences);
            IEnumerable<EventOccurrence> eventOccurrencesDelta = await _dbContext.EventOccurrences.ReturnEventOccurrenceDeltaAsync(eventOccurrences, cancellationToken);

            if (!eventOccurrencesDelta.Any() || eventOccurrencesDelta is null)
                throw new AlreadyExistsException($"{nameof(EventOccurrence)}/s with stated date already exist.");

            _dbContext.EventOccurrences.AddRange(eventOccurrencesDelta);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<IEnumerable<EventOccurrenceDto>>(eventOccurrencesDelta);
        }
    }
}
