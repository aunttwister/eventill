using Microsoft.AspNetCore.Authorization;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Reservations.Api.Authorization
{
    internal class ProfileAuthorizationHandler : AuthorizationHandler<ProfileAuthorizationRequirement>
    {
        private readonly IReservationDbContext _dbContext;

        public ProfileAuthorizationHandler(IReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, ProfileAuthorizationRequirement requirement)
        {
            var userEmail = context?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
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
    }
}
