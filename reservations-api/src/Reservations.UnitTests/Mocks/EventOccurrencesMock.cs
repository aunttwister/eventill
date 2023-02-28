using Microsoft.EntityFrameworkCore;
using Moq;
using Reservations.Application.UnitTests.Common;
using Reservations.Domain.Enums;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.UnitTests.Mocks
{
    public class EventOccurrencesMock : IDataMock<EventOccurrence>
    {
        public Mock<DbSet<EventOccurrence>> MockData()
        {
            IQueryable<EventOccurrence> eventOccurrences = InitializeData().AsQueryable();

            return MockDbSetProvider.CreateMockFromIQueryable(eventOccurrences);
        }

        public List<EventOccurrence> InitializeData()
        {
            return new List<EventOccurrence> {
                new EventOccurrence() { Id = 1, StartTime = DateTime.Parse("2023-02-08 18:00:00"), EventId = 1, IsActive = true },
                new EventOccurrence() { Id = 2, StartTime = DateTime.Parse("2023-02-15 18:00:00"), EventId = 2, IsActive = true },
                new EventOccurrence() { Id = 3, StartTime = DateTime.Parse("2023-02-23 18:00:00"), EventId = 3, IsActive = true },
                new EventOccurrence() { Id = 4, StartTime = DateTime.Parse("2023-02-28 18:00:00"), EventId = 4, IsActive = true }
            };
        }
    }
}
