using AutoMapper;
using MediatR;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Helpers;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.EventOccurrences.Commands.CreateMultipleEventOccurrences;
using Reservations.Application.Questions.Commands.CreateMultipleQuestions;
using Reservations.Application.Tickets.Commands.CreateMultipleTickets;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Commands.CreateEvent
{
    public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand, EventDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        public CreateEventCommandHandler(
            IMediator mediator,
            IMapper mapper,
            IReservationDbContext dbContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<EventDto> Handle(CreateEventCommand request, CancellationToken cancellationToken)
        {
            Event newEvent = _mapper.Map<Event>(request);

            if (request.EventTypeId is null)
                if (await _dbContext.EventTypes.EventTypeExistsAsync(newEvent.EventType, cancellationToken))
                    throw new AlreadyExistsException($"{nameof(EventType)} with the same name already exists.");

            newEvent.EventOccurences = await FilterEventOccurrencesAsync(newEvent.EventOccurences, cancellationToken);

            foreach (EventOccurrence eventOccurrence in newEvent.EventOccurences)
            {
                eventOccurrence.Tickets.AddRange(
                    InitializeTickets(request.TicketCount, request.TicketPrice));
            }

            newEvent.Questions = await FilterQuestionsAsync(newEvent.Questions, cancellationToken);

            _dbContext.Events.Add(newEvent);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventDto>(newEvent);
        }

        private async Task<List<EventOccurrence>> FilterEventOccurrencesAsync(
            IEnumerable<EventOccurrence> eventOccurrences,
            CancellationToken cancellationToken)
        {
            IEnumerable<EventOccurrence> eventOccurrencesDelta = await _dbContext.EventOccurrences
                .ReturnEventOccurrenceDeltaAsync(eventOccurrences, cancellationToken);

            if (!eventOccurrencesDelta.Any() || eventOccurrencesDelta is null)
                throw new AlreadyExistsException($"{nameof(EventOccurrence)}/s with stated date already exist.");

            return eventOccurrencesDelta.ToList();
        }

        private List<Ticket> InitializeTickets(int ticketCount, decimal ticketPrice)
        {
            List<Ticket> tickets = new List<Ticket>();
            for (int i = 0; i < ticketCount; i++)
            {
                tickets.Add(new Ticket() { Price = ticketPrice });
            }

            return tickets;
        }

        private async Task<List<Question>> FilterQuestionsAsync(
            IEnumerable<Question> questions,
            CancellationToken cancellationToken)
        {
            IEnumerable<Question> questionsDelta = await _dbContext.Questions.ReturnQuestionDeltaAsync(questions, cancellationToken);

            if (!questionsDelta.Any() || questionsDelta is null)
                throw new AlreadyExistsException($"{nameof(Question)}/s with stated date already exist.");

            return questionsDelta.ToList();
        }
    }
}
