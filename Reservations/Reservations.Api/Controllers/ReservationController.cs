using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Reservations.Commands.CreateReservation;
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
        [ProducesResponseType(typeof(List<ReservationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand request)
        {
            var log = await _mediator.Send(request);
            return Ok(log);
        }
    }
}
