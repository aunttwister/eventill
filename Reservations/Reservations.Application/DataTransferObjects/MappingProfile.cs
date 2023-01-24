using AutoMapper;
using Reservations.Application.Common.Helpers;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.DataTransferObjects
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<Reservation, ReservationDto>()
                .ForMember(rc => rc.FirstName, opts => opts.MapFrom(r => r.User.FirstName))
                .ForMember(rc => rc.LastName, opts => opts.MapFrom(r => r.User.LastName))
                .ForMember(rc => rc.Email, opts => opts.MapFrom(r => r.User.Email))
                .ForMember(rc => rc.TicketCount, opts => opts.MapFrom(r => r.Tickets.Count));
            CreateMap<Question, QuestionDto>();
            CreateMap<EventOccurrence, EventOccurrenceDto>()
                .ForMember(eo => eo.StartTime, opts => opts.MapFrom(eo => eo.StartTime.RemoveYear()));
            CreateMap<Ticket, TicketDto>();
            CreateMap<List<Ticket>, TicketsStateDto>()
                .ForMember(ts => ts.Count, opts => opts.MapFrom(t => t.Count))
                .ForMember(ts => ts.Price, opts => opts.MapFrom(t => t.Select(t => t.Price).First()));
            CreateMap<Event, EventDto>()
                .ForMember(e => e.Length, opts => opts.MapFrom(e => new DateTime(e.Length).ToString("HH:mm")))
                .ForMember(e => e.EventOccurrences, opts => opts.MapFrom(m => m.EventOccurrences));
            CreateMap<EventType, EventTypeDto>();
        }
    }
}
