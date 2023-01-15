using AutoMapper;
using Reservations.Domain;
using System;
using System.Collections.Generic;
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
            CreateMap<Question, EventOccurrenceDto>();
            CreateMap<EventOccurrence, EventOccurrenceDto>();
            CreateMap<Ticket, TicketDto>();
            CreateMap<List<Ticket>, TicketsStateDto>()
                .ForMember(ts => ts.Count, opts => opts.MapFrom(t => t.Count))
                .ForMember(ts => ts.Price, opts => opts.MapFrom(t => t.Select(t => t.Price).First()));
            CreateMap<Event, EventDto>();
            CreateMap<EventType, EventTypeDto>();
        }
    }
}
