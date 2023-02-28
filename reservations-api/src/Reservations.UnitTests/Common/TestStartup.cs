using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using Reservations.UnitTests.Mocks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.UnitTests.Common
{
    public class TestStartup
    {
        public virtual ServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();
            // Configure services
            services.AddSingleton<IDataMock<Role>, RolesMock>();
            services.AddSingleton<IDataMock<User>, UsersMock>();
            services.AddSingleton<IDataMock<Reservation>, ReservationsMock>();
            services.AddSingleton<IDataMock<Ticket>, TicketsMock>();
            services.AddSingleton<IDataMock<EventOccurrence>, EventOccurrencesMock>();
            services.AddSingleton<IDataMock<Question>, QuestionsMock>();
            services.AddSingleton<IDataMock<Event>, EventsMock>();
            services.AddSingleton<IDataMock<EventType>, EventTypesMock>();

            return services.BuildServiceProvider();
        }

        protected IMapper ConfigureAutoMapper()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
                mc.AddProfile(new Application.Reservations.MappingProfile());
                mc.AddProfile(new Application.EventOccurrences.MappingProfile());
                mc.AddProfile(new Application.Events.MappingProfile());
                mc.AddProfile(new Application.Questions.MappingProfile());
                mc.AddProfile(new Application.Users.MappingProfile());
                mc.AddProfile(new Application.EventOccurrences.MappingProfile());
                mc.AddProfile(new Application.EventTypes.MappingProfile());
            });
            return mappingConfig.CreateMapper();
        }
    }
}
