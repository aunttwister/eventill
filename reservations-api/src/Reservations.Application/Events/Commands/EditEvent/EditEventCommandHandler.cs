﻿using AutoMapper;
using MediatR;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Helpers;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Commands.EditEvent
{
    public class EditEventCommandHandler : IRequestHandler<EditEventCommand>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        private readonly IEventSetupService _eventSetupService;
        public EditEventCommandHandler(
            IMapper mapper,
            IReservationDbContext dbContext,
            IEventSetupService eventSetupService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _eventSetupService = eventSetupService;
        }
        public async Task<Unit> Handle(EditEventCommand request, CancellationToken cancellationToken)
        {
            Event newEvent = _mapper.Map<Event>(request);

            if (request.EventTypeId is null)
                if (await _dbContext.EventTypes.EventTypeExistsAsync(newEvent.EventType, cancellationToken))
                    throw new AlreadyExistsException($"{nameof(EventType)} with the same name already exists.");


            foreach (EventOccurrence eventOccurrence in newEvent.EventOccurrences)
            {
                eventOccurrence.Tickets.AddRange(
                    _eventSetupService.InitializeTickets(request.TicketCount, request.TicketPrice));
            }

            if (newEvent.Questions is not null && newEvent.Questions.Count > 0)
            {
                newEvent.Questions.Clear();
                newEvent.Questions = await _eventSetupService
                    .FilterQuestionsAsync(newEvent.Questions, cancellationToken);
            }

            _dbContext.Events.Update(newEvent);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
