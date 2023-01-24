using MediatR;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Users.Commands.CreateGuestUser
{
    public class CreateGuestUserCommand : IRequest<User>
    {
        public CreateGuestUserCommand(string name, string email, string phoneNumber)
        {
            Name = name;
            Email = email;
            PhoneNumber = phoneNumber;
        }

        public CreateGuestUserCommand()
        {

        }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
