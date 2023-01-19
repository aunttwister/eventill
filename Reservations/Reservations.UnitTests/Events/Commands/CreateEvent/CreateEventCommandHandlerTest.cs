using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.Common.Services;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.EventOccurrences.Commands.CreateEventOccurrence;
using Reservations.Application.Events.Commands.CreateEvent;
using Reservations.Application.EventTypes.Commands.CreateEventType;
using Reservations.Application.Questions.Commands.CreateQuestion;
using Reservations.Domain;
using Reservations.UnitTests.Common;
using Reservations.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reservations.UnitTests.Events.Commands.CreateEvent

{
    public class CreateEventCommandHandlerTest : TestStartup
    {
        private readonly IMapper _mapper;
        private readonly IDataMock<Question> _questionDataMocks;
        private readonly IDataMock<EventOccurrence> _eventOccurrenceDataMocks;
        private readonly IDataMock<Event> _eventDataMocks;
        private readonly IDataMock<EventType> _eventTypeMocks;

        public CreateEventCommandHandlerTest()
        {
            _mapper = ConfigureAutoMapper();

            var serviceProvider = ConfigureServices();

            _questionDataMocks = serviceProvider.GetRequiredService<IDataMock<Question>>();
            _eventOccurrenceDataMocks = serviceProvider.GetRequiredService<IDataMock<EventOccurrence>>();
            _eventDataMocks = serviceProvider.GetRequiredService<IDataMock<Event>>();
            _eventTypeMocks = serviceProvider.GetRequiredService<IDataMock<EventType>>();
        }

        [Fact]
        public async Task CreateEvent_Success_AllDependants_Inserted_Result()
        {
            //arrange
            var mockContext = new Mock<IReservationDbContext>();
            mockContext.Setup(c => c.Questions).Returns(_questionDataMocks.MockData().Object);
            mockContext.Setup(c => c.EventTypes).Returns(_eventTypeMocks.MockData().Object);
            mockContext.Setup(c => c.Events).Returns(_eventDataMocks.MockData().Object);
            mockContext.Setup(c => c.EventOccurrences).Returns(_eventOccurrenceDataMocks.MockData().Object);
            var mockDateTime = new Mock<IDateTime>();
            mockDateTime.Setup(mock => mock.UtcNow).Returns(() => DateTime.UtcNow);

            IEventSetupService mockEventSetupService = new EventSetupService(mockContext.Object);

            string userEmail = "member1@example.com";

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(mock => mock.Email).Returns(() => userEmail);
            mockUserService.Setup(mock => mock.IsAuthenticated).Returns(() => true);
            var mockMediator = new Mock<IMediator>();
            var handler = new CreateEventCommandHandler(_mapper, mockContext.Object, mockEventSetupService);

            CreateEventCommand mockRequest = new CreateEventCommand
            {

                TicketCount = 15,
                Description = "xxxx",
                Length = new TimeSpan(1, 30, 0),
                EventOccurences = new List<CreateEventOccurrenceCommand>()
                {
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-02 18:00:00"), EndTime = DateTime.Parse("2023-03-02 19:30:00"), EventId = 1 },
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-09 18:00:00"), EndTime = DateTime.Parse("2023-03-09 19:30:00"), EventId = 1 },
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-16 18:00:00"), EndTime = DateTime.Parse("2023-03-16 19:30:00"), EventId = 1 },
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-23 18:00:00"), EndTime = DateTime.Parse("2023-03-23 19:30:00"), EventId = 1 },
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-30 18:00:00"), EndTime = DateTime.Parse("2023-03-30 19:30:00"), EventId = 1 },
                },
                EventQuestions = new List<CreateQuestionCommand>()
                {
                    new CreateQuestionCommand() { Content = "xxxx" },
                    new CreateQuestionCommand() { Content = "yyyy" }
                },
                EventType = new CreateEventTypeCommand()
                {
                    Name = "EventTypeNew"
                }
            };

            //act
            var result = await handler.Handle(mockRequest, new CancellationToken());

            //assert

            // Check that each method was only called once.
            mockContext.Verify(x => x.Events.Add(It.IsAny<Event>()), Times.Once());

            //Check if the response type and it's nested objects are as expected

            Assert.IsType<EventDto>(result);

            Assert.IsType<EventTypeDto>(result.EventType);

            Assert.IsType<EventOccurrenceDto>(result.EventOccurences.First());

            Assert.IsType<QuestionDto>(result.Questions.First());

            Assert.IsType<TicketDto>(result.EventOccurences.First().Tickets.First());

            //Check if there is an expected number of entities in a response.

            Assert.True(
                Assert.IsType<EventOccurrenceDto>(result.EventOccurences.First()).Tickets.Count() == mockRequest.TicketCount);

            Assert.True(
                Assert.IsType<EventDto>(result).Questions.Count() == mockRequest.EventQuestions.Count());

            Assert.True(
                Assert.IsType<EventDto>(result).EventOccurences.Count() == mockRequest.EventOccurences.Count());
        }

        [Fact]
        public async Task CreateEvent_Fail_DuplicateEventType_Inserted()
        {
            //arrange
            var mockContext = new Mock<IReservationDbContext>();
            mockContext.Setup(c => c.Questions).Returns(_questionDataMocks.MockData().Object);
            mockContext.Setup(c => c.EventTypes).Returns(_eventTypeMocks.MockData().Object);
            mockContext.Setup(c => c.Events).Returns(_eventDataMocks.MockData().Object);
            mockContext.Setup(c => c.EventOccurrences).Returns(_eventOccurrenceDataMocks.MockData().Object);
            var mockDateTime = new Mock<IDateTime>();
            mockDateTime.Setup(mock => mock.UtcNow).Returns(() => DateTime.UtcNow);

            IEventSetupService mockEventSetupService = new EventSetupService(mockContext.Object);

            string userEmail = "member1@example.com";

            var mockUserService = new Mock<ICurrentUserService>();
            mockUserService.Setup(mock => mock.Email).Returns(() => userEmail);
            mockUserService.Setup(mock => mock.IsAuthenticated).Returns(() => true);
            var mockMediator = new Mock<IMediator>();

            var handler = new CreateEventCommandHandler(_mapper, mockContext.Object, mockEventSetupService);

            CreateEventCommand mockRequest = new CreateEventCommand
            {

                TicketCount = 15,
                Description = "xxxx",
                Length = new TimeSpan(1, 30, 0),
                EventOccurences = new List<CreateEventOccurrenceCommand>()
                {
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-02 18:00:00"), EndTime = DateTime.Parse("2023-03-02 19:30:00"), EventId = 1 },
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-09 18:00:00"), EndTime = DateTime.Parse("2023-03-09 19:30:00"), EventId = 1 },
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-16 18:00:00"), EndTime = DateTime.Parse("2023-03-16 19:30:00"), EventId = 1 },
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-23 18:00:00"), EndTime = DateTime.Parse("2023-03-23 19:30:00"), EventId = 1 },
                    new CreateEventOccurrenceCommand() { StartTime = DateTime.Parse("2023-03-30 18:00:00"), EndTime = DateTime.Parse("2023-03-30 19:30:00"), EventId = 1 },
                },
                EventQuestions = new List<CreateQuestionCommand>()
                {
                    new CreateQuestionCommand() { Content = "xxxx" },
                    new CreateQuestionCommand() { Content = "yyyy" }
                },
                EventType = new CreateEventTypeCommand()
                {
                    Name = "Ambient Performance"
                }
            };

            //act
            async Task result() => await handler.Handle(mockRequest, new CancellationToken());

            //assert

            await Assert.ThrowsAsync<AlreadyExistsException>(result);
        }
    }
}