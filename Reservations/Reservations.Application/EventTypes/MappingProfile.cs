using AutoMapper;
using Reservations.Application.EventOccurrences.Commands.CreateEventOccurrence;
using Reservations.Application.EventTypes.Commands.CreateEventType;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventTypes
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEventTypeCommand, EventType>();
        }
    }
}
