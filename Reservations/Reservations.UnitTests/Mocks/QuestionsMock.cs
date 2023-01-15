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
    public class QuestionsMock : IDataMock<Question>
    {
        public Mock<DbSet<Question>> MockData()
        {
            IQueryable<Question> questions = InitializeData().AsQueryable();

            return MockDbSetProvider.CreateMockFromIQueryable(questions);
        }

        public List<Question> InitializeData()
        {
            return new List<Question> {
                new Question { Id = 1, Content = "xxx" },
                new Question { Id = 2, Content = "yyy" },
                new Question { Id = 3, Content = "zzz" },
                new Question { Id = 4, Content = "ddd" },
                new Question { Id = 5, Content = "www" },
                new Question { Id = 6, Content = "vvv" }
            };
        }
    }
}
