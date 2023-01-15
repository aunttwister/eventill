using MediatR;
using Reservations.Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.EventTypes.Commands.CreateEventType
{
    public class CreateEventTypeCommand : IRequest<EventTypeDto>
    {
        public string Name { get; set; }
    }
}
