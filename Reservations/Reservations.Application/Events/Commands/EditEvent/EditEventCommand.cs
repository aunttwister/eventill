using MediatR;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.EventOccurrences.Commands.CreateEventOccurrence;
using Reservations.Application.EventTypes.Commands.CreateEventType;
using Reservations.Application.Questions.Commands.CreateQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Commands.EditEvent
{
    public class EditEventCommand : IRequest
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //Stored as ticks
        public TimeSpan Length { get; set; }
        public int TicketCount { get; set; }
        public decimal TicketPrice { get; set; }
        public int? EventTypeId { get; set; }
        public CreateEventTypeCommand EventType { get; set; }
        public IEnumerable<CreateQuestionCommand> EventQuestions { get; set; }
        public IEnumerable<CreateEventOccurrenceCommand> EventOccurences { get; set; }
    }
}
