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
    public class EventsMock : IDataMock<Event>
    {
        public List<Event> InitializeData()
        {
            return new List<Event>() { };
        }

        public Mock<DbSet<Event>> MockData()
        {
            IQueryable<Event> events = InitializeData().AsQueryable();

            return MockDbSetProvider.CreateMockFromIQueryable(events);
        }
    }
}
