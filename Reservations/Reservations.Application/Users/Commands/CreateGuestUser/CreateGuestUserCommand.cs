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
        public CreateGuestUserCommand(string firstName, string lastName, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
        }

        public CreateGuestUserCommand()
        {

        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
