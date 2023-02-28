using AutoMapper;
using MediatR;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Questions.Commands.CreateMultipleQuestions;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Tickets.Commands.CreateMultipleTickets
{
    public class CreateMultipleTicketsCommandHandler : IRequestHandler<CreateMultipleTicketsCommand, IEnumerable<TicketDto>>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        public CreateMultipleTicketsCommandHandler(
            IMapper mapper,
            IReservationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TicketDto>> Handle(CreateMultipleTicketsCommand request, CancellationToken cancellationToken)
        {
            //if (await _dbContext.Tickets.TicketsCountExceedsAsync(request.TicketCount, request.EventOccurrence.Id, cancellationToken))
            //    throw new Exception(); //new exception to be created

            List<Ticket> newTickets = new List<Ticket>();
            for (int i = 0; i < request.TicketCount; i++)
            {
                newTickets.Add(new Ticket()
                {
                    Price = request.Price,
                    EventOccurence = request.EventOccurrence,
                    TicketState = TicketState.Available
                });
            }

            _dbContext.Tickets.AddRange(newTickets);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<IEnumerable<TicketDto>>(newTickets);
        }
    }
}
