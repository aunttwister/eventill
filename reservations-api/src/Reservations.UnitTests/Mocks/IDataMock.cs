using Microsoft.EntityFrameworkCore;
using Moq;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.UnitTests.Mocks
{
    public interface IDataMock<T> where T : class
    {
        public Mock<DbSet<T>> MockData();
        public List<T> InitializeData();
    }
}
