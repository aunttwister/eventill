﻿using AutoMapper;
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

namespace Reservations.Application.DataTransferObjects
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
                .ForMember(u => u.Role, opts => opts.MapFrom(u => u.Role.Name));
            CreateMap<Reservation, ReservationDto>()
                .ForMember(rc => rc.Name, opts => opts.MapFrom(r => r.User.Name))
                .ForMember(rc => rc.Email, opts => opts.MapFrom(r => r.User.Email))
                .ForMember(rc => rc.EventOccurrence, opts => opts.Ignore())
                .ForMember(rc => rc.Tickets, opts => opts.MapFrom(r => r.EventOccurrence.Tickets.Where(t => t.ReservationId == r.Id)))
                .ForMember(rc => rc.TicketCount, opts => opts.MapFrom(r => r.EventOccurrence.Tickets.Where(t => t.ReservationId == r.Id).Count()))
                .ReverseMap();
            CreateMap<Question, QuestionDto>();
            CreateMap<EventOccurrence, EventOccurrenceDto>()
                .ForMember(eo => eo.StartTime, opts => opts.MapFrom(eo => eo.StartTime))
                .ForMember(eo => eo.TotalTicketCount, opts => opts.MapFrom(eo => eo.Tickets.Count))
                .ForMember(eo => eo.AvailableTicketCount, opts => opts.MapFrom(eo => eo.Tickets
                .Where(t => t.TicketState == TicketState.Available).ToList().Count))
                .ForMember(eo => eo.ReservedTicketCount, opts => opts.MapFrom(eo => eo.Tickets
                    .Where(t => t.TicketState == TicketState.Reserved).ToList().Count))
                .ForMember(eo => eo.SoldTicketCount, opts => opts.MapFrom(eo => eo.Tickets
                    .Where(t => t.TicketState == TicketState.Sold).ToList().Count));
            CreateMap<Ticket, TicketDto>().ReverseMap();
            CreateMap<List<Ticket>, TicketsStateDto>()
                .ForMember(ts => ts.Count, opts => opts.MapFrom(t => t.Count))
                .ForMember(ts => ts.Price, opts => opts.MapFrom(t => t.Select(t => t.Price).FirstOrDefault()));
            CreateMap<Event, EventDto>()
                .ForMember(e => e.Length, opts => opts.MapFrom(e => new DateTime(e.Length).ToString("HH:mm")))
                .ForMember(e => e.EventOccurrences, opts => opts.MapFrom(m => m.EventOccurrences));
            CreateMap<EventType, EventTypeDto>();
        }
    }
}
