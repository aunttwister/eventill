using MediatR;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Destructurama.Attributed;

namespace Reservations.Application.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<UserDto>
    {
        /// <summary>
        /// User's email address (mandatory)
        /// </summary>
        [LogMasked(PreserveLength = true)]
        public string Email { get; set; }
        /// <summary>
        /// Flag to include user's roles (default false)
        /// </summary>
        public bool IncludeRoles { get; set; }
    }
}
