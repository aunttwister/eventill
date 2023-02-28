using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using Reservations.Security.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Authentication.Commands.AuthenticateUser
{
    public class AuthenticateUserCommandHandler : IRequestHandler<AuthenticateUserCommand, object>
    {
        private readonly IReservationDbContext _dbContext; //this dependency is to be removed when ISecurityDbContext becomes active
        //private readonly ISecurityDbContext _dbContext; //the idea is to use this in the future
        private readonly IAuthenticationService _authenticationService;
        private readonly ITokenProviderService _tokenProviderService;
        public AuthenticateUserCommandHandler(
            IReservationDbContext dbContext, 
            IAuthenticationService authenticationService,
            ITokenProviderService tokenProviderService)
        {
            _dbContext = dbContext;
            _authenticationService = authenticationService;
            _tokenProviderService = tokenProviderService;
        }

        public async Task<object> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
        {
            User user = await _dbContext.Users.Include(u => u.LoginDetails).Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == request.Email);

            bool verifyResponse = _authenticationService.Verify(request.Password, user.LoginDetails.Salt, user.LoginDetails.HashPassword);
            if (verifyResponse == false)
                throw new UnauthorizedAccessException();

            return _tokenProviderService.GenerateToken(user.Id, user.Role.Name);
        }
    }
}
