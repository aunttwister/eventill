using MediatR;
using System.Security.Claims;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.Common.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Reservations.Application.Users.Queries.GetUserByEmail;
using Reservations.Security.Common.Interfaces;

namespace Reservations.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private UserDto _currentUser;
        private readonly IMediator _mediator;
        private readonly ITokenProviderService _tokenProviderService;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="mediator"></param>
        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IMediator mediator, ITokenProviderService tokenProviderService)
        {
            _tokenProviderService = tokenProviderService;
            _mediator = mediator;
            RegisterClaims(httpContextAccessor);
            Email = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            IsAuthenticated = Email != null;
        }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Is authenticated
        /// </summary>
        public bool IsAuthenticated { get; }

        /// <summary>
        /// Retrieves user profile from the database
        /// </summary>
        /// <returns></returns>
        public async Task<UserDto> GetCurrentUserAsync()
        {
            if (_currentUser == null && IsAuthenticated)
            {
                try
                {
                    _currentUser = await _mediator.Send(new GetUserByEmailQuery
                    {
                        Email = Email,
                        IncludeRoles = true
                    });
                }
                catch (NotFoundException)
                {
                    // ignore exception if user wasn't found, in that case it would be null
                }
            }

            return _currentUser;
        }

        private void RegisterClaims(IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty);
            httpContextAccessor.HttpContext.User = _tokenProviderService.ExtractClaims(token);
        }
    }
}
