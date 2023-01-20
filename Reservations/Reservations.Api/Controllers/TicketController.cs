using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Tickets.Commands.CreateMultipleTickets;
using Reservations.Application.Tickets.Queries.GetCountTicketState;
using Reservations.Domain.Enums;

namespace Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{eventOccurrenceId}/{ticketState}")]
        [ProducesResponseType(typeof(TicketsStateDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetCountTicketState([FromRoute] long eventOccurrenceId, [FromRoute] string ticketState)
        {

            TicketsStateDto ticketStateResponse = await _mediator.Send(new GetCountTicketStateQuery(eventOccurrenceId, ticketState));
            return Ok(ticketStateResponse);
        }

        [HttpPost]
        [ProducesResponseType(typeof(List<TicketDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateTickets([FromBody] CreateMultipleTicketsCommand request)
        {
            var log = await _mediator.Send(request);
            return Ok(log);
        }
    }
}
