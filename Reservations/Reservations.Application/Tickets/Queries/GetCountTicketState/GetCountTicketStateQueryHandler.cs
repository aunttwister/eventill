using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Tickets.Queries.GetCountTicketState
{
    public class GetCountTicketStateQueryHandler : IRequestHandler<GetCountTicketStateQuery, TicketsStateDto>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        public GetCountTicketStateQueryHandler(
            IMediator mediator,
            IMapper mapper,
            IReservationDbContext dbContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _dbContext = dbContext;
        }

        public async Task<TicketsStateDto> Handle(GetCountTicketStateQuery request, CancellationToken cancellationToken)
        {
            IEnumerable<Ticket> reqestedStateTickets = await _dbContext.Tickets
                .Where(t => t.TicketState == request.TicketState.StringToTicketState())
                .ToListAsync(cancellationToken);

            return _mapper.Map<TicketsStateDto>(reqestedStateTickets);
        }
    }
}
