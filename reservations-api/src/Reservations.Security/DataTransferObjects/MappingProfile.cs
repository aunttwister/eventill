using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Application.Common.Helpers;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.DataTransferObjects
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.Role, opts => opts.MapFrom(u => u.Role.Name));
            CreateMap<LoginDetails, LoginDetailsDto>();
        }
    }
}
