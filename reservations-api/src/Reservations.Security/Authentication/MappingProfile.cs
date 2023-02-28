using AutoMapper;
using Reservations.Domain;
using Reservations.Security.Authentication.Commands.CreateAdmin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Authentication
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateAdminCommand, User>();
        }
    }
}
