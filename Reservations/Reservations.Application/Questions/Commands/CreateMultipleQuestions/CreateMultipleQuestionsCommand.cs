using MediatR;
using Reservations.Application.DataTransferObjects;
using Reservations.Application.Questions.Commands.CreateQuestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Questions.Commands.CreateMultipleQuestions
{
    public class CreateMultipleQuestionsCommand : IRequest<IEnumerable<TicketDto>>
    {
        public IEnumerable<CreateQuestionCommand> Questions;
    }
}
