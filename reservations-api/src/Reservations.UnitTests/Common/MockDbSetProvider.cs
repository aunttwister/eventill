using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using Reservations.Domain;

namespace Reservations.Application.UnitTests.Common
{
    public static class MockDbSetProvider
    {
        public static Mock<DbSet<T>> CreateMockFromList<T>(List<T> data) where T: class
        {
            var dataQueryable = data.AsQueryable();

            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new AsyncEnumerator<T>(dataQueryable.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new AsyncQueryProvider<T>(dataQueryable.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(dataQueryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(dataQueryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => dataQueryable.GetEnumerator());

            return mockSet;
        }

        public static Mock<DbSet<T>> CreateMockFromIQueryable<T>(IQueryable<T> queryable) where T : class
        {
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IAsyncEnumerable<T>>()
                .Setup(m => m.GetAsyncEnumerator(new CancellationToken()))
                .Returns(new AsyncEnumerator<T>(queryable.GetEnumerator()));

            mockSet.As<IQueryable<T>>()
                .Setup(m => m.Provider)
                .Returns(new AsyncQueryProvider<T>(queryable.Provider));

            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return mockSet;
        }
    }
}
