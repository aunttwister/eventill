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
    public class ReservationsMock : IDataMock<Reservation>
    {
        private readonly IDataMock<User> _userDataMocks;
        private readonly IDataMock<Ticket> _ticketDataMocks;
        public ReservationsMock(
            IDataMock<User> userDataMocks, 
            IDataMock<Ticket> ticketDataMocks)
        {
            _userDataMocks = userDataMocks;
            _ticketDataMocks = ticketDataMocks;
        }
        public Mock<DbSet<Reservation>> MockData()
        {
            IQueryable<Reservation> reservations = InitializeData().AsQueryable();

            return MockDbSetProvider.CreateMockFromIQueryable(reservations);
        }

        public List<Reservation> InitializeData()
        {
            List<Ticket> tickets = _ticketDataMocks.InitializeData();
            List<User> users = _userDataMocks.InitializeData();

            return new List<Reservation>() {
                new Reservation { Id = 1, UserId = users.First().Id, User = users.First(), Tickets = tickets.Where(t => t.TicketState == TicketState.Reserved).ToList() },
                new Reservation { Id = 2, UserId = users.Last().Id, User = users.Last(), Tickets = tickets.Where(t => t.TicketState == TicketState.Sold).ToList() }
            };
        }
    }
}
