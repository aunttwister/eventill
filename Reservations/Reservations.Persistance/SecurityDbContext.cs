using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using Reservations.Persistance.Configurations;
using Reservations.Security.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Persistance
{
    public class SecurityDbContext : DbContextExtend, ISecurityDbContext
    {
        public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options) { }

        public SecurityDbContext(DbContextOptions<SecurityDbContext> options,
            IDateTime datetime,
            ICurrentUserService userService) : base(options, datetime, userService) { }
        public DbSet<User> Users { get; set; }
        public DbSet<LoginDetails> LoginDetails { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Configurations.SecurityConfiguration.RoleConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SecurityConfiguration.UserConfiguration());
            modelBuilder.ApplyConfiguration(new Configurations.SecurityConfiguration.LoginDetailsConfiguration());
            modelBuilder.Ignore<Reservation>();
        }
    }
}
