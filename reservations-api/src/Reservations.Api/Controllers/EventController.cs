using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Events.Commands.CreateEvent;
using Reservations.Application.Events.Commands.DeleteEvent;
using Reservations.Application.Events.Commands.EditEvent;
using Reservations.Application.Events.Queries.GetEventById;
using Reservations.Application.Events.Queries.GetEvents;

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
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> CreateEventAsync([FromBody] CreateEventCommand request)
        {
            var response = await _mediator.Send(request);
            return CreatedAtAction(nameof(GetEventAsync), new { Id = response.Id });
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> EditEventAsync([FromRoute] long id, [FromBody] EditEventCommand request)
        {
            if (id != request.Id)
            {
                return BadRequest("Event from route doesn't match the ID from request body");
            }

            await _mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> DeleteEventAsync([FromRoute] long id)
        {
            await _mediator.Send(new DeleteEventCommand { Id = id });
            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<EventDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventsAsync([FromQuery] GetEventsQuery request)
        {
            var logs = await _mediator.Send(request);
            return Ok(logs);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventAsync([FromRoute] long id, [FromQuery] GetEventByIdQuery request)
        {
            request.Id = id;
            var log = await _mediator.Send(request);
            return Ok(log);
        }
    }
}
