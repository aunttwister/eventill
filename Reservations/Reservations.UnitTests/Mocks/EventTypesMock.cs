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
    public class EventTypesMock : IDataMock<EventType>
    {
        public List<EventType> InitializeData()
        {
            return new List<EventType>()
            {
                new EventType() { Id = 1, Name = "Ambient Performance" },
                new EventType() { Id = 2, Name = "Theatre Performance" }
            };
        }

        public Mock<DbSet<EventType>> MockData()
        {
            IQueryable<EventType> eventTypes = InitializeData().AsQueryable();

            return MockDbSetProvider.CreateMockFromIQueryable(eventTypes);
        }
    }
}
