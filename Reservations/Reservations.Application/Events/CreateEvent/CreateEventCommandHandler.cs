using AutoMapper;
using MediatR;
using Reservations.Application.Common.Extensions;
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

            _dbContext.Events.Add(newEvent);

            await _dbContext.SaveChangesAsync(cancellationToken);

            await _mediator.Send(
                new CreateMultipleQuestionsCommand() 
                { 
                    Questions = request.EventQuestions 
                }, cancellationToken);

            await _mediator.Send(
                new CreateMultipleEventOccurrenceCommand() 
                { 
                    EventOccurrences = request.EventOccurences 
                }, cancellationToken);

            await _mediator.Send(
                new CreateMultipleTicketsCommand() 
                { 
                    TicketCount = request.TicketCount, 
                    Price = request.TicketPrice 
                }, cancellationToken);

            return _mapper.Map<EventDto>(newEvent);
        }
    }
}
