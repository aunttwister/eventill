using Microsoft.EntityFrameworkCore;
using Moq;
using Reservations.Application.UnitTests.Common;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.UnitTests.Mocks
{
    public class UsersMock : IDataMock<User>
    {
        private readonly IDataMock<Role> _roleDataMocks;
        public UsersMock(IDataMock<Role> roleDataMocks)
        {
            _roleDataMocks = roleDataMocks;
        }
        public Mock<DbSet<User>> MockData()
        {
            IQueryable<User> users = InitializeData().AsQueryable();

            return MockDbSetProvider.CreateMockFromIQueryable(users);
        }

        public List<User> InitializeData()
        {
            IEnumerable<Role> roles = _roleDataMocks.InitializeData();
            Role roleGuest = roles.First(r => r.Name == "Guest");
            Role roleAdmin = roles.First(r => r.Name == "Admin");

            return new List<User> {
                    new User { Id = new Guid("08d99adb-cc82-447e-8238-528f0668d03d"), FirstName = "John", LastName = "Doe", Email = "john@doe.com", Role = roleGuest, RoleId = roleGuest.Id },
                    new User { Id = new Guid("08d9a2c1-0527-4965-8b2f-1a8f8e775d1f"), FirstName = "Jane", LastName = "Doe", Email = "jane@doe.com", Role = roleAdmin, RoleId = roleAdmin.Id },
                    new User { Id = new Guid("05d9b7c1-0527-4122-8b2f-1a8f8e375d1f"), FirstName = "Jack", LastName = "Doe", Email = "jack@doe.com", Role = roleGuest, RoleId = roleGuest.Id }
            };
        }
    }
}
