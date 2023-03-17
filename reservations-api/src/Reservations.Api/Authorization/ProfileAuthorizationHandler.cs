using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using Reservations.Security.Common.Interfaces;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Reservations.Api.Authorization
{
    internal class ProfileAuthorizationHandler : AuthorizationHandler<ProfileAuthorizationRequirement>
    {
        private readonly IReservationDbContext _dbContext;
        private readonly ITokenProviderService _tokenProviderService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ProfileAuthorizationHandler(IReservationDbContext dbContext, ITokenProviderService tokenProviderService, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _tokenProviderService = tokenProviderService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ProfileAuthorizationRequirement requirement)
        {
            RegisterClaims(_httpContextAccessor);
            var userEmail = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userEmail))
            {
                User user = null;
                try
                {

                    user = await _dbContext.Users.GetUserByEmailAsync(userEmail);
                }
                catch (NotFoundException)
                {
                    // ignore exception
                }


                if (user != null)
                {
                    context.Succeed(requirement);
                }
            }
        }


        private void RegisterClaims(IHttpContextAccessor httpContextAccessor)
        {
            var token = httpContextAccessor.HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", string.Empty).Replace("\"", string.Empty);
            httpContextAccessor.HttpContext.User = _tokenProviderService.ExtractClaims(token);
        }
    }
}
