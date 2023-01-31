using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Reservations;
using Reservations.Application.Reservations.Commands.CreateReservation;
using Reservations.Domain;
using Reservations.UnitTests.Common;
using Reservations.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reservations.UnitTests.Reservations.Commands
{
    public class CreateReservationCommandHandlerTest : TestStartup
    {
        private readonly IMapper _mapper;
        private readonly IDataMock<Reservation> _reservationDataMocks;
        private readonly IDataMock<Ticket> _ticketDataMocks;
        private readonly IDataMock<User> _userDataMocks;

        public CreateReservationCommandHandlerTest()
        {
            _mapper = ConfigureAutoMapper();

            var serviceProvider = ConfigureServices();

            _reservationDataMocks = serviceProvider.GetRequiredService<IDataMock<Reservation>>();
            _ticketDataMocks = serviceProvider.GetRequiredService<IDataMock<Ticket>>();
            _userDataMocks = serviceProvider.GetRequiredService<IDataMock<User>>();
        }

        [Fact]
        public async Task CreateReservation_Success_Result()
        {
            var mockContext = new Mock<IReservationDbContext>();
            mockContext.Setup(c => c.Reservations).Returns(_reservationDataMocks.MockData().Object);
            mockContext.Setup(c => c.Tickets).Returns(_ticketDataMocks.MockData().Object);
            mockContext.Setup(c => c.Users).Returns(_userDataMocks.MockData().Object);
            var mockDateTime = new Mock<IDateTime>();
            mockDateTime.Setup(mock => mock.UtcNow).Returns(() => DateTime.UtcNow);

            string userEmail = "member1@example.com";
            string name = "Member";

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(mock => mock.Email).Returns(() => userEmail);
            mockUserService.Setup(mock => mock.IsAuthenticated).Returns(() => true);
            var mockMediator = new Mock<IMediator>();
            var handler = new CreateReservationCommandHandler(mockMediator.Object, _mapper, mockContext.Object);

            var result = await handler.Handle(new CreateReservationCommand
            {
                Email = userEmail,
                Name = name,
                TicketCount = 1

            }, new CancellationToken());

            // Check that each method was only called once.
            mockContext.Verify(x => x.Reservations.Add(It.IsAny<Reservation>()), Times.Once());

            Assert.IsType<ReservationDto>(result);
        }

        [Fact]
        public async Task CreateReservation_Throws_OrderExceedsAvailableAmount_Exception()
        {
            //arrange
            var mockContext = new Mock<IReservationDbContext>();
            mockContext.Setup(c => c.Reservations).Returns(_reservationDataMocks.MockData().Object);
            mockContext.Setup(c => c.Tickets).Returns(_ticketDataMocks.MockData().Object);
            mockContext.Setup(c => c.Users).Returns(_userDataMocks.MockData().Object);
            var mockDateTime = new Mock<IDateTime>();
            mockDateTime.Setup(mock => mock.UtcNow).Returns(() => DateTime.UtcNow);

            string userEmail = "member1@example.com";
            string name = "Member";

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(mock => mock.Email).Returns(() => userEmail);
            mockUserService.Setup(mock => mock.IsAuthenticated).Returns(() => true);
            var mockMediator = new Mock<IMediator>();
            var handler = new CreateReservationCommandHandler(mockMediator.Object, _mapper, mockContext.Object);

            //act

            async Task result() => await handler.Handle(new CreateReservationCommand
            {
                Email = userEmail,
                Name = name,
                TicketCount = 20

            }, new CancellationToken());

            //assert

            await Assert.ThrowsAsync<BadRequestException>(result);
        }
    }
}
