using AutoMapper;
using MediatR;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations.Commands.EditReservation
{
    public class EditReservationCommandHandler : IRequestHandler<EditReservationCommand>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;
        public EditReservationCommandHandler(IReservationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(EditReservationCommand request, CancellationToken cancellationToken)
        {
            Reservation reservation = _mapper.Map<Reservation>(request);
            if (!await _dbContext.Reservations.ReservationExistsAsync(reservation))
                throw new NotFoundException(nameof(Reservation), request.Id);

            _dbContext.Reservations.Update(reservation);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
