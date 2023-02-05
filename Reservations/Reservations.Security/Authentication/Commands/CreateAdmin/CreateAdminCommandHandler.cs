using AutoMapper;
using MediatR;
using Reservations.Domain;
using Reservations.Security.Common.Extensions;
using Reservations.Security.Common.Interfaces;
using Reservations.Security.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Authentication.Commands.CreateAdmin
{
    public class CreateAdminCommandHandler : IRequestHandler<CreateAdminCommand, UserDto>
    {
        private readonly ISecurityDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;
        public CreateAdminCommandHandler(
            ISecurityDbContext dbContext, 
            IMapper mapper, 
            IAuthenticationService authenticationService)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }
        public async Task<UserDto> Handle(CreateAdminCommand request, CancellationToken cancellationToken)
        {
            User user = _mapper.Map<User>(request);

            var (hashPassword, salt) = _authenticationService.GenerateHash(request.Password);
            user.LoginDetails = new LoginDetails()
            {
                Salt = salt,
                HashPassword = hashPassword
            };

            user.Role = await _dbContext.Roles.GetAdminRoleAsync();

            _dbContext.Users.Add(user);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<UserDto>(user);
        }
    }
}
