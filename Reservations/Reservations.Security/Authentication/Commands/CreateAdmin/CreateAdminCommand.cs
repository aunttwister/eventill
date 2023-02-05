using MediatR;
using Reservations.Domain;
using Reservations.Security.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Authentication.Commands.CreateAdmin
{
    public class CreateAdminCommand : IRequest<UserDto>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
    }
}
