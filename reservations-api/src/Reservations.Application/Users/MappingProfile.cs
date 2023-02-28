using AutoMapper;
using Reservations.Application.Users.Commands.CreateGuestUser;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Users
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateGuestUserCommand, User>();
        }
    }
}
