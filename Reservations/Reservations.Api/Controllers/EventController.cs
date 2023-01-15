using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Events.Commands.CreateEvent;
using Reservations.Application.Reservations.Commands.CreateReservation;

namespace Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventCommand request)
        {
            var log = await _mediator.Send(request);
            return Ok(log);
        }
    }
}
