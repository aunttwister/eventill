using MediatR;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.EventOccurrences.Commands.CreateEventOccurrence;
using Reservations.Application.Questions.Commands.CreateQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventOccurrences.Commands.CreateMultipleEventOccurrences
{
    public class CreateMultipleEventOccurrenceCommand : IRequest<IEnumerable<EventOccurrenceDto>>
    {
        public IEnumerable<CreateEventOccurrenceCommand> EventOccurrences;
    }
}
