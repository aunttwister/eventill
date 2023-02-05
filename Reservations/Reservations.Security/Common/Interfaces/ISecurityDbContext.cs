using Microsoft.EntityFrameworkCore;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Security.Common.Interfaces
{
    public interface ISecurityDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<LoginDetails> LoginDetails { get; set; }
        public DbSet<Role> Roles { get; set; }
    }
}
