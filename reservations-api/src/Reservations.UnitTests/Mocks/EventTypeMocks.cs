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
    public class EventTypeMocks : IDataMock<EventType>
    {
        public Mock<DbSet<EventType>> MockData()
        {
            IQueryable<EventType> eventTypes = InitializeData().AsQueryable();

            return MockDbSetProvider.CreateMockFromIQueryable(eventTypes);
        }

        public List<EventType> InitializeData()
        {
            return new List<EventType> {
                new EventType() { Id = 1, Name = "EventTypeOne" },
                new EventType() { Id = 2, Name = "EventTypeTwo" }
            };
        }
    }
}
