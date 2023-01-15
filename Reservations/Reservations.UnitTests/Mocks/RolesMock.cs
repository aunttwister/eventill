using Microsoft.EntityFrameworkCore;
using Moq;
using Reservations.Application.UnitTests.Common;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.UnitTests.Mocks
{
    public class RolesMock : IDataMock<Role>
    {
        public List<Role> InitializeData()
        {
            return new List<Role>() {
                new Role { Id = 1, Name = "Admin" },
                new Role { Id = 2, Name = "Guest" }
            };
        }

        public Mock<DbSet<Role>> MockData()
        {
            IQueryable<Role> roles = InitializeData().AsQueryable();

            return MockDbSetProvider.CreateMockFromIQueryable(roles);
        }
    }
}
