using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Events.Commands.DeleteEvent;
using Reservations.Application.Events.Queries.GetEventById;
using Reservations.Application.Reservations.Commands.ConfirmPaymentCompleted;
using Reservations.Application.Reservations.Commands.CreateReservation;
using Reservations.Application.Reservations.Commands.DeleteReservation;
using Reservations.Application.Reservations.Commands.EditMultipleReservation;
using Reservations.Application.Reservations.Queries.GetReservations;
using Reservations.Application.Tickets.Commands.CreateMultipleTickets;
using Reservations.Application.Tickets.Queries.GetCountTicketState;

namespace Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ReservationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReservationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ReservationDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> CreateReservationAsync([FromBody] CreateReservationCommand request)
        {
            var log = await _mediator.Send(request);
            return Ok(log);
        }

        [HttpPost("confirm")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> ConfirmPaymentReservationsAsync([FromBody] ConfirmPaymentCompletedCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]

        public async Task<IActionResult> DeleteReservationAsync([FromRoute] long id)
        {
            await _mediator.Send(new DeleteReservationCommand { Id = id });
            return NoContent();
        }

        [HttpGet("{eventOccurrenceId}")]
        [ProducesResponseType(typeof(List<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetReservationsAsync([FromRoute] long eventOccurrenceId)
        {
            var log = await _mediator.Send(new GetReservationsQuery { EventOccurrenceId = eventOccurrenceId });
            return Ok(log);
        }

        [HttpPost("edit/multiple")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> EditMultipleReservationsAsync([FromBody] EditMultipleReservationsCommand request)
        {
            await _mediator.Send(request);
            return NoContent();
        }
    }
}
