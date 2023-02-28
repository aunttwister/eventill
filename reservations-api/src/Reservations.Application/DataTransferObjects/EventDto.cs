using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.DataTransferObjects
{
    public class EventDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string DirectorName { get; set; }
        public string Description { get; set; }
        public string Length { get; set; }
        public int EventTypeId { get; set; }
        public EventTypeDto EventType { get; set; }
        public IEnumerable<QuestionDto> Questions { get; set; }
        public IEnumerable<EventOccurrenceDto> EventOccurrences { get; set; }
    }
}
