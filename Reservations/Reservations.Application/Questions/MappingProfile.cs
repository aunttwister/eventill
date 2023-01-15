using AutoMapper;
using Reservations.Application.Events.Commands.CreateEvent;
using Reservations.Application.Questions.Commands;
using Reservations.Application.Questions.Commands.CreateQuestion;
using Reservations.Application.Reservations.Commands.CreateReservation;
using Reservations.Application.Users.Commands.CreateGuestUser;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Questions
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateQuestionCommand, Question>();
        }
    }
}
