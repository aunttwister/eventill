using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Users.Commands.CreateGuestUser;
using Reservations.Domain;
using Reservations.Domain.Enums;

namespace Reservations.Application.Reservations.Commands.CreateReservation
{
    public class CreateReservationCommandHandler : IRequestHandler<CreateReservationCommand, ReservationDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        public CreateReservationCommandHandler(
            IMediator mediator, 
            IMapper mapper,
            IReservationDbContext dbContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<ReservationDto> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Ticket> availableTickets = await _dbContext.Tickets
                .Where(t => t.TicketState == TicketState.Available)
                .ToListAsync(cancellationToken);

            if (availableTickets.Count() < request.TicketCount)
                throw new OrderExceedsAvailabileAmountException($"Order exceeds available number of {nameof(Ticket)}/s.");

            User user = await _mediator.Send(new CreateGuestUserCommand(
                request.FirstName,
                request.LastName,
                request.Email), cancellationToken);

            Reservation reservation = _mapper.Map<Reservation>(request);

            reservation.User = user;

            List<Ticket> ticketsToBeReserved = availableTickets.Take(request.TicketCount).ToList();

            ticketsToBeReserved.ForEach(t => t.TicketState = TicketState.Reserved);

            reservation.Tickets = ticketsToBeReserved;

            _dbContext.Reservations.Add(reservation);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<ReservationDto>(reservation);
        }
    }
}
