using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Application.DataTransferObjects;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Questions.Commands.CreateMultipleQuestions
{
    public class CreateMultipleQuestionsCommandHandler : IRequestHandler<CreateMultipleQuestionsCommand, IEnumerable<QuestionDto>>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        public CreateMultipleQuestionsCommandHandler(
            IMapper mapper,
            IReservationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<QuestionDto>> Handle(CreateMultipleQuestionsCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Question> questions = _mapper.Map<IEnumerable<Question>>(request.Questions);
            IEnumerable<Question> questionsDelta = await _dbContext.Questions.ReturnQuestionDeltaAsync(questions, cancellationToken);

            if (!questionsDelta.Any() || questionsDelta is null)
                throw new AlreadyExistsException($"{nameof(Question)}/s with stated date already exist.");

            _dbContext.Questions.AddRange(questionsDelta);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<IEnumerable<QuestionDto>>(questionsDelta);
        }
    }
}
