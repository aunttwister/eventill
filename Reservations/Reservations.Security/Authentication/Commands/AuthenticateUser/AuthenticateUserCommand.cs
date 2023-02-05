using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Authentication.Commands.AuthenticateUser
{
    public class AuthenticateUserCommand : IRequest<object>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
