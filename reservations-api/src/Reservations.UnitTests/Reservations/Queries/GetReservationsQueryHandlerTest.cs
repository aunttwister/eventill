using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Reservations.Commands.CreateReservation;
using Reservations.Application.Reservations.Queries.GetReservations;
using Reservations.Domain;
using Reservations.UnitTests.Common;
using Reservations.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reservations.UnitTests.Reservations.Queries
{
    public class GetReservationsQueryHandlerTest : TestStartup
    {
        private readonly IMapper _mapper;
        private readonly IDataMock<Reservation> _reservationDataMocks;
        private readonly IDataMock<Ticket> _ticketDataMocks;
        private readonly IDataMock<User> _userDataMocks;
        private readonly IDataMock<EventOccurrence> _eventOccurrenceDataMocks;

        public GetReservationsQueryHandlerTest()
        {
            _mapper = ConfigureAutoMapper();

            var serviceProvider = ConfigureServices();

            _reservationDataMocks = serviceProvider.GetRequiredService<IDataMock<Reservation>>();
            _ticketDataMocks = serviceProvider.GetRequiredService<IDataMock<Ticket>>();
            _userDataMocks = serviceProvider.GetRequiredService<IDataMock<User>>();
            _eventOccurrenceDataMocks = serviceProvider.GetRequiredService<IDataMock<EventOccurrence>>();
        }

        [Fact]
        public async Task RetrieveReservation_Success_Result()
        {
            var mockContext = new Mock<IReservationDbContext>();
            mockContext.Setup(c => c.Reservations).Returns(_reservationDataMocks.MockData().Object);
            mockContext.Setup(c => c.Tickets).Returns(_ticketDataMocks.MockData().Object);
            mockContext.Setup(c => c.Users).Returns(_userDataMocks.MockData().Object);
            mockContext.Setup(c => c.EventOccurrences).Returns(_eventOccurrenceDataMocks.MockData().Object);
            var mockDateTime = new Mock<IDateTime>();
            mockDateTime.Setup(mock => mock.UtcNow).Returns(() => DateTime.UtcNow);

            string userEmail = "member1@example.com";

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(mock => mock.Email).Returns(() => userEmail);
            mockUserService.Setup(mock => mock.IsAuthenticated).Returns(() => true);
            var mockMediator = new Mock<IMediator>();
            var handler = new GetReservationsQueryHandler(mockContext.Object, _mapper);

            var result = await handler.Handle(new GetReservationsQuery
            {
                EventOccurrenceId = 1

            }, new CancellationToken());

            // Check that each method was only called once.

            Assert.True(
                Assert.IsType<List<ReservationDto>>(result).Count == 3);

            Assert.IsType<List<ReservationDto>>(result);
        }
    }
}
