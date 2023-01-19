using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Events.Commands.DeleteEvent
{
    public class DeleteEventCommand : IRequest
    {
        public long Id { get; set; }
    }
}
