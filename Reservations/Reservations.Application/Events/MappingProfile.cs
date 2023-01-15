using AutoMapper;
using Reservations.Application.Events.Commands.CreateEvent;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateEventCommand, Event>()
                .ForMember(m => m.Length, opts => opts.MapFrom(e => e.Length.Ticks))
                .ForMember(m => m.EventOccurences, opts => opts.MapFrom(e => e.EventOccurences.ToList()))
                .ForMember(m => m.Questions, opts => opts.MapFrom(e => e.EventQuestions.ToList()));
        }
    }
}
