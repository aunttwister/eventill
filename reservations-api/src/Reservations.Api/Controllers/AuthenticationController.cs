using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Events.Commands.CreateEvent;
using Reservations.Security.Authentication.Commands.AuthenticateUser;
using Reservations.Security.Authentication.Commands.CreateAdmin;

namespace Reservations.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> AuthenticateUserAsync([FromBody] AuthenticateUserCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
        [HttpPost("create/admin")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<IActionResult> CreateAdminAsync(CreateAdminCommand request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
