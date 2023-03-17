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

namespace Reservations.Application.Users.Commands.CreateGuestUser
{
    public class CreateGuestUserCommandHandler : IRequestHandler<CreateGuestUserCommand, User>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateGuestUserCommandHandler(
            IReservationDbContext dbContext,
            IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<User> Handle(CreateGuestUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _dbContext.Users.GetUserByEmailAsync(
                email: request.Email, 
                cancellationToken: cancellationToken);

            if (user != null)
            {
                return user;
            }
            else
            {
                User newUser = _mapper.Map<User>(request);
                newUser.Role = await _dbContext.Roles.GetGuestRoleAsync(cancellationToken);
                _dbContext.Users.Add(newUser);

                return newUser;
            }
        }
    }
}
