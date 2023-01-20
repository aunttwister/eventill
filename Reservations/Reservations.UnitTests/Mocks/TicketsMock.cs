using Microsoft.EntityFrameworkCore;
using Moq;
using Reservations.Application.UnitTests.Common;
using Reservations.Domain;
using Reservations.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.UnitTests.Mocks
{
    public class TicketsMock : IDataMock<Ticket>
    {
        public Mock<DbSet<Ticket>> MockData()
        {
            IQueryable<Ticket> tickets = InitializeData().AsQueryable();

            return MockDbSetProvider.CreateMockFromIQueryable(tickets);
        }

        public List<Ticket> InitializeData()
        {
            return new List<Ticket> {
                new Ticket { Id = 1, Price = 1499.99m, TicketState = TicketState.Available, EventOccurenceId = 1},
                new Ticket { Id = 2, Price = 1499.99m, TicketState = TicketState.Available, EventOccurenceId = 1},
                new Ticket { Id = 3, Price = 1499.99m, TicketState = TicketState.Available, EventOccurenceId = 1},
                new Ticket { Id = 4, Price = 1499.99m, TicketState = TicketState.Available, EventOccurenceId = 1},
                new Ticket { Id = 5, Price = 1499.99m, TicketState = TicketState.Available, EventOccurenceId = 1},
                new Ticket { Id = 6, Price = 1499.99m, TicketState = TicketState.Unavailable, EventOccurenceId = 1},
                new Ticket { Id = 7, Price = 1499.99m, TicketState = TicketState.Unavailable, EventOccurenceId = 1},
                new Ticket { Id = 8, Price = 1499.99m, TicketState = TicketState.Unavailable, EventOccurenceId = 1},
                new Ticket { Id = 9, Price = 1499.99m, TicketState = TicketState.Reserved, EventOccurenceId = 1, ReservationId = 1 },
                new Ticket { Id = 10, Price = 1499.99m, TicketState = TicketState.Reserved, EventOccurenceId = 1, ReservationId = 1 },
                new Ticket { Id = 11, Price = 1499.99m, TicketState = TicketState.Reserved, EventOccurenceId = 1, ReservationId = 3 },
                new Ticket { Id = 12, Price = 1499.99m, TicketState = TicketState.Reserved, EventOccurenceId = 1, ReservationId = 3 },
                new Ticket { Id = 10, Price = 1499.99m, TicketState = TicketState.Reserved, EventOccurenceId = 2, ReservationId = 2 },
                new Ticket { Id = 11, Price = 1499.99m, TicketState = TicketState.Reserved, EventOccurenceId = 2, ReservationId = 2 },
                new Ticket { Id = 12, Price = 1499.99m, TicketState = TicketState.Reserved, EventOccurenceId = 2, ReservationId = 2 },
                new Ticket { Id = 13, Price = 1499.99m, TicketState = TicketState.Sold, EventOccurenceId = 1 },
                new Ticket { Id = 14, Price = 1499.99m, TicketState = TicketState.Sold, EventOccurenceId = 1 },
                new Ticket { Id = 15, Price = 1499.99m, TicketState = TicketState.Sold, EventOccurenceId = 1 }
            };
        }
    }
}
