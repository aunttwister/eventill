using Microsoft.EntityFrameworkCore;
using Reservations.Application.Common.Exceptions;
using Reservations.Application.Common.Extensions;
using Reservations.Application.Common.Interfaces;
using Reservations.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Services
{
    public class EventSetupService : IEventSetupService
    {
        private readonly IReservationDbContext _dbContext;
        public EventSetupService(IReservationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<EventOccurrence>> FilterEventOccurrencesAsync(
            IEnumerable<EventOccurrence> eventOccurrences, 
            CancellationToken cancellationToken)
        {
            IEnumerable<EventOccurrence> eventOccurrencesDelta = await _dbContext.EventOccurrences
                .ReturnEventOccurrenceDeltaAsync(eventOccurrences, cancellationToken);

            if (!eventOccurrencesDelta.Any() || eventOccurrencesDelta is null)
                throw new AlreadyExistsException($"{nameof(EventOccurrence)}/s with stated date already exist.");

            return eventOccurrencesDelta.ToList();
        }

        public async Task<List<Question>> FilterQuestionsAsync(
            IEnumerable<Question> questions, 
            CancellationToken cancellationToken)
        {
            IEnumerable<Question> questionsDelta = await _dbContext.Questions
                .ReturnQuestionDeltaAsync(questions, cancellationToken);

            if (!questionsDelta.Any() || questionsDelta is null)
                throw new AlreadyExistsException($"{nameof(Question)}/s with stated date already exist.");

            return questionsDelta.ToList();
        }

        public List<Ticket> InitializeTickets(int ticketCount, decimal ticketPrice)
        {
            List<Ticket> tickets = new List<Ticket>();
            for (int i = 0; i < ticketCount; i++)
            {
                tickets.Add(new Ticket() { Price = ticketPrice });
            }

            return tickets;
        }
    }
}
