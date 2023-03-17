using Microsoft.EntityFrameworkCore;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Extensions
{
    public static class UserDbExtensions
    {
        public static async Task<User> GetUserByEmailAsync(
            this DbSet<User> users, string email, bool includeRoles = false, CancellationToken cancellationToken = default)
        {
            IQueryable<User> userQuery = users.AsQueryable();

            if (includeRoles)
            {
                userQuery = userQuery.Include(u => u.Role);
            }

            return await userQuery
                .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        }
    }
}
