using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.EventOccurrences.Commands.EditMultipleEventOccurrences;
using Reservations.Application.EventOccurrences.Queries.GetEventOccurrenceById;

namespace Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventOccurrenceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public EventOccurrenceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(EventOccurrenceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventOccurrenceAsync([FromRoute] long id)
        {
            var log = await _mediator.Send(new GetEventOccurrenceByIdQuery { Id = id });
            return Ok(log);
        }

        [HttpPost("edit/multiple")]
        [ProducesResponseType(typeof(List<EventOccurrenceDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> EditMultipleEventOccurrencesAsync([FromBody] EditMultipleEventOccurrencesCommand request)
        {
            var log = await _mediator.Send(request);
            return Ok(log);
        }
    }
}
