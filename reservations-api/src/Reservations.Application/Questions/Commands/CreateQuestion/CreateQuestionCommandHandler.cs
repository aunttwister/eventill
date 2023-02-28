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

namespace Reservations.Application.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, EventOccurrenceDto>
    {
        private readonly IMapper _mapper;
        private readonly IReservationDbContext _dbContext;
        public CreateQuestionCommandHandler(
            IMapper mapper,
            IReservationDbContext dbContext)
        {
            _mapper = mapper;
            _dbContext = dbContext;
        }
        public async Task<EventOccurrenceDto> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            Question newQuestion = _mapper.Map<Question>(request);

            if (await _dbContext.Questions.QuestionExistsAsync(newQuestion, cancellationToken))
                throw new AlreadyExistsException($"{nameof(Question)}/s with stated date already exist.");

            _dbContext.Questions.Add(newQuestion);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return _mapper.Map<EventOccurrenceDto>(newQuestion);
        }
    }
}
