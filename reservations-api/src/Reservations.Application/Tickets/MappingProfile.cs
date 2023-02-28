using AutoMapper;
using Reservations.Application.Tickets.Commands.EditTicket;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Tickets
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<EditTicketCommand, Ticket>();
        }
    }
}
