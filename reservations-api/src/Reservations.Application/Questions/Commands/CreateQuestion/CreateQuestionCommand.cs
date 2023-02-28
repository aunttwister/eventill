using MediatR;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<EventOccurrenceDto>
    {
        public string Content { get; set; }
    }
}
