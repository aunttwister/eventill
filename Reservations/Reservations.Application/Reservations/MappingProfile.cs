using AutoMapper;
using Reservations.Application.Reservations.Commands.ConfirmPaymentCompleted;
using Reservations.Application.Reservations.Commands.CreateReservation;
using Reservations.Application.Reservations.Commands.EditReservation;
using Reservations.Application.Users.Commands.CreateGuestUser;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Reservations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateReservationCommand, Reservation>();
            CreateMap<EditReservationCommand, Reservation>();
        }
    }
}
