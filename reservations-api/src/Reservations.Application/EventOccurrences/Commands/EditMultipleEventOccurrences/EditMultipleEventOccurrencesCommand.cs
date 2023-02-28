using MediatR;
using Reservations.Application.EventOccurrences.Commands.EditEventOccurrence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventOccurrences.Commands.EditMultipleEventOccurrences
{
    public class EditMultipleEventOccurrencesCommand : IRequest
    {
        public IEnumerable<EditEventOccurrenceCommand> EventOccurrences { get; set; }
    }
}
