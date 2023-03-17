using AutoMapper;
using MediatR;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Users.Queries.GetUserByEmail
{
    internal class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, UserDto>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetUserByEmailQueryHandler(IReservationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<UserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users.GetUserByEmailAsync(
                email: request.Email,
                includeRoles: request.IncludeRoles,
                cancellationToken: cancellationToken
            );

            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Email);
            }

            return _mapper.Map<UserDto>(user);
        }
    }
}
