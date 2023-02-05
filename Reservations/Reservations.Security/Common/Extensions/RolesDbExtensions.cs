using Microsoft.EntityFrameworkCore;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Common.Extensions
{
    public static class RolesDbExtensions
    {
        public static async Task<Role> GetGuestRoleAsync(this DbSet<Role> roles, CancellationToken cancellationToken = default)
        {
            return await roles.FirstOrDefaultAsync(r => r.Name == "Guest");
        }
        public static async Task<Role> GetAdminRoleAsync(this DbSet<Role> roles, CancellationToken cancellationToken = default)
        {
            return await roles.FirstOrDefaultAsync(r => r.Name == "Admin");
        }
    }
}
